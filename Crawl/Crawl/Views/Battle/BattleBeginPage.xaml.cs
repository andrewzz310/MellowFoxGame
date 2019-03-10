using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Views;
using Crawl.GameEngine;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleBeginPage : ContentPage
	{

        //viewmodel of battle with character and monster data for binding
        private BattleViewModel _instanceC;

        // battle engine
        BattleEngine mbattleEngine = new BattleEngine();

        //round engine
        RoundEngine mRoundEngine = new RoundEngine();

        //  think about passing characters or something else instead of game engines
        public BattleBeginPage ()
		{

            InitializeComponent();
            BindingContext = _instanceC = BattleViewModel.Instance;

        }
        // Passing in the battleviewmodel 
        public BattleBeginPage(BattleViewModel _instance)
        {
            InitializeComponent();

            _instanceC = _instance;

            _instanceC.StartBattle();
            // Load the Characters into the Battle Engine
            _instanceC.LoadCharacters();
            Debug.WriteLine("Battle Start" + " Characters :" + _instanceC.BattleEngine.CharacterList.Count);
            _instanceC.StartRound();
            Debug.WriteLine("Round Start" + " Monsters:" + _instanceC.BattleEngine.MonsterList.Count);

            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
           
            //debug for monsters
            Debug.WriteLine("Monsters are Below :");

            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].FormatOutput());
                Debug.WriteLine("asdasfasdfasfsafsdfasdfsfa");
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].Head);
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].Feet);
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].Necklass);
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].RightFinger);
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].LeftFinger);
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].Feet);
               

            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("########################################");


            //debug for characters
            Debug.WriteLine("Charcters are Below :");

            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(_instanceC.BattleEngine.CharacterList[i].FormatOutput());
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("########################################");
        }

        // For now use this to begin the battle for testing purposes once a character is selected
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
        
            // Put the 6 characters into a list based on the characters that are selected
            mbattleEngine.AddCharactersToBattle(_instanceC);
            //init console message and characters
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
          //  Debug.WriteLine("The Mellow Fox Game Battle has Started" + " Total Characters :" + a.CharacterList.Count);
            Debug.WriteLine("Characters are Below :");
            
            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(mbattleEngine.CharacterList[i].FormatOutput());
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("########################################");
            Debug.WriteLine("Round has now started");

            string outputString = "Battle Has added these characters" +
          mbattleEngine.CharacterList[0].FormatOutput() + Environment.NewLine +
          mbattleEngine.CharacterList[1].FormatOutput() + "sadf";

            // start round is the next step in here 3/4/ 6:07 pm
            mbattleEngine.StartRound(_instanceC);

            // maybe use battlengine inside of viewmodel and just pass viewmodel

  
            // the pop up for either cancel or see the score details
            var action = await DisplayActionSheet(outputString,
                "Cancel",
                null,
                "View Score Results");

            
        }

        //Shows monster that was selected
        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            //do something for click like attack or something
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            //Manually deselect item.
            MonstersBattle.SelectedItem = null;
        }


        //When message says round is over, display round over page
        private async void RoundOver_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoundOver());
        }

        // when game over, display game over page
        private async void GameOver_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameOver());
        }

        //do this to display next turn button 
        public async void Turn_Command(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "RoundNextTurn");

            // Hold the current state
            var CurrentRoundState = _instanceC.BattleEngine.RoundStateEnum;

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                _instanceC.NewRound();
                // MessagingCenter.Send(this, "NewRound");

                Debug.WriteLine("!!!!!!!!!!!!!!New Round :" + _instanceC.BattleEngine.BattleScore.RoundCount);

                //ShowModalPageMonsterList();
            }

            // Check for Game Over
            if (CurrentRoundState == RoundEnum.GameOver)
            {
                _instanceC.EndBattle();
                //MessagingCenter.Send(this, "EndBattle");
                Debug.WriteLine("End Battle!!!!!!!!!!!!!!!!!!!");

                // Output Formatted Results 
               // var myResult = _instanceC.BattleEngine.GetResultsOutput();
                //Debug.Write(myResult);

                // output results of the game after battle is over
                var myResult = _instanceC.BattleEngine.GetResultsOutput();
                var myScore = _instanceC.BattleEngine.GetScoreValue();
                Debug.Write(myResult);


                var outputString = "Mellow Fox Battle Over!";

                // the pop up for either cancel or see the score details
                var action = await DisplayActionSheet(outputString,
                    "Cancel",
                    null,
                    "View Score Results");

                // Show the results
                if (action == "View Score Results")
                {
                    var myScoreObject = _instanceC.BattleEngine.GetScoreObject();
                    await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
                }




                // Let the user know the game is over
                //   ClearMessages();    // Clear message
                //  AppendMessage("Game Over\n"); // Show Game Over

                // Clear the players from the center of the board
                //DrawGameBoardClear();

                // Change to the Game Over Button
                // GameNextButton.IsVisible = false;
                //GameOverButton.IsVisible = true;

                return;
            }

            // Output the Game Board
            //DrawGameBoardAttackerDefender();

            // Output The Message that happened.
          //  GameMessage();

            _instanceC.RoundNextTurn();
            // change this TODO this is just a place holder
            //await Navigation.PushAsync(new BattleBeginPage());
        }

        /*
        /// <summary>
        /// Append new message in front of old message (makes it a stack from top down)
        /// </summary>
        /// <param name="message"></param>
        public void AppendMessage(string message)
        {
            MessageText.Text = message + "\n" + MessageText.Text;
        }



        public void ClearMessages()
        {
            MessageText.Text = "";
            htmlSource.Html = _viewModel.BattleEngine.BattleMessages.GetHTMLBlankMessage();
            HtmlBox.Source = htmlSource;
        }
        */

        bool hasAppearedOnce = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;


            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_instanceC.DatasetChars.Count == 0 || _instanceC.DatasetMons.Count == 0 || 
                _instanceC.DatasetItems.Count == 0)
            {
                _instanceC.LoadDataCommand.Execute(null);
            }
            else if (_instanceC.NeedsRefresh())
            {
                _instanceC.LoadDataCommand.Execute(null);
            }

            BindingContext = _instanceC;
        }
    }
}