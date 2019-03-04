using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;
using Crawl.GameEngine;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleMainPage : ContentPage
    {

        // Hold the Selected Characters
        BattleCharacterSelectPage _myModalCharacterSelectPage;

        // Hold the Monsters
        BattleMonsterListPage _myModalBattleMonsterListPage;

        // HTML Formatting for message output box
        HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Hold the Veiw Model
        private BattleViewModel _viewModel;

        /// <summary>
        /// Stand up the Page and initiate state
        /// </summary>
        public BattleMainPage()
        {
            InitializeComponent();

            // Show the Next button, hide the Game Over button
            GameNextButton.IsVisible = true;
            GameOverButton.IsVisible = false;

            MessagingCenter.Send(this, "StartBattle");
            Debug.WriteLine("Battle Start" + " Characters :" + BattleViewModel.Instance.BattleEngine.CharacterList.Count);

            // Load the Characters into the Battle Engine
            MessagingCenter.Send(this, "LoadCharacters");

            // Start the Round
            MessagingCenter.Send(this, "StartRound");
            Debug.WriteLine("Round Start" + " Monsters:" + BattleViewModel.Instance.BattleEngine.MonsterList.Count);

            BindingContext = _viewModel = BattleViewModel.Instance;

            // Clear the Screen
            ClearMessages();
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            MessageText.Text = "";
            htmlSource.Html = @"<html><body></body></html>";
            HtmlBox.Source = htmlSource;
        }

        /// <summary>
        /// Next Turn Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnNextClicked(object sender, EventArgs args)
        {
            // Do the turn...
            MessagingCenter.Send(this, "RoundNextTurn");

            // Hold the current state
            var CurrentRoundState = _viewModel.BattleEngine.RoundStateEnum;

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                MessagingCenter.Send(this, "NewRound");

                Debug.WriteLine("New Round :" + _viewModel.BattleEngine.BattleScore.RoundCount);

                ShowModalPageMonsterList();
            }

            // Check for Game Over
            if (CurrentRoundState == RoundEnum.GameOver)
            {
                MessagingCenter.Send(this, "EndBattle");
                Debug.WriteLine("End Battle");

                // Output Formatted Results 
                var myResult = _viewModel.BattleEngine.GetResultsOutput();
                Debug.Write(myResult);

                // Let the user know the game is over
                ClearMessages();    // Clear message
                AppendMessage("Game Over\n"); // Show Game Over

                // Change to the Game Over Button
                GameNextButton.IsVisible = false;
                GameOverButton.IsVisible = true;

                return;
            }

            // Output The Message that happened.
            GameMessage();
        }

        /// <summary>
        /// Append new message in front of old message (makes it a stack from top down)
        /// </summary>
        /// <param name="message"></param>
        public void AppendMessage(string message)
        {
            MessageText.Text = message + "\n" + MessageText.Text;
        }

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {

            var message = _viewModel.BattleEngine.BattleMessages.TurnMessage;
            Debug.WriteLine("Message: " + message);

            AppendMessage(message);

            htmlSource.Html = _viewModel.BattleEngine.BattleMessages.GetHTMLFormattedTurnMessage();
            HtmlBox.Source = HtmlBox.Source = htmlSource;

        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void OnGameOverClicked(object sender, EventArgs args)
        {
            var myScore = _viewModel.BattleEngine.BattleScore.ScoreTotal;
            var outputString = "Battle Over! Score " + myScore.ToString();
            Debug.WriteLine(outputString);

            var myScoreObject = _viewModel.BattleEngine.BattleScore;
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));

            // Back up to the Start of Battle
            await Navigation.PopToRootAsync();
        }

        private void HandleModalPopping(object sender, ModalPoppingEventArgs e)
        {
            if (e.Modal == _myModalBattleMonsterListPage)
            {
                _myModalBattleMonsterListPage = null;

                // remember to remove the event handler:
                Crawl.App.Current.ModalPopping -= HandleModalPopping;
            }

            if (e.Modal == _myModalCharacterSelectPage)
            {
                _myModalCharacterSelectPage = null;

                // remember to remove the event handler:
                Crawl.App.Current.ModalPopping -= HandleModalPopping;
            }
        }

        private async void ShowModalPageMonsterList()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalBattleMonsterListPage = new BattleMonsterListPage();
            await Navigation.PushModalAsync(_myModalBattleMonsterListPage);
        }

        private async void ShowModalPageCharcterSelect()
        {
            // When you want to show the modal page, just call this method
            // add the event handler for to listen for the modal popping event:
            Crawl.App.Current.ModalPopping += HandleModalPopping;
            _myModalCharacterSelectPage = new BattleCharacterSelectPage();
            await Navigation.PushModalAsync(_myModalCharacterSelectPage);
        }
    }

}