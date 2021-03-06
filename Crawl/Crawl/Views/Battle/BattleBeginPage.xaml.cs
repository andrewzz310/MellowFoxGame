﻿using System;
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


        // HTML Formatting for message output box
        HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        //  think about passing characters or something else instead of game engines
        public BattleBeginPage()
		{

            _instanceC = BattleViewModel.Instance;
            InitializeComponent();


            _instanceC.StartBattle();
            // Load the Characters into the Battle Engine
            _instanceC.LoadCharacters();
            Debug.WriteLine("Battle Start" + " Characters :" + _instanceC.BattleEngine.CharacterList.Count);
            _instanceC.StartRound();
            Debug.WriteLine("Round Start" + " Monsters:" + _instanceC.BattleEngine.MonsterList.Count);

            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);

            BindingContext = _instanceC;
       

            //debug for monsters
            Debug.WriteLine("Monsters are Below :");

            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(_instanceC.BattleEngine.MonsterList[i].FormatOutput());



            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("########################################");


            //debug for characters
            Debug.WriteLine("Charcters are Below :");

            //characters below 
            for (var i = 0; i < _instanceC.BattleEngine.CharacterList.Count; i++)
            {
                Debug.WriteLine(_instanceC.BattleEngine.CharacterList[i].FormatOutput());
             
                    
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("########################################");
        }
       
        
        // this method just for displaying characters selected for battle
        private void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {

            var data = args.SelectedItem as Character;
            if (data == null)
                return;
            //await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

           

        }

        //Shows monster that was selected
        private  void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

           
           // await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

         
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

            // go to the next turn
            _instanceC.RoundNextTurn();
            //MessagingCenter.Send(this, "RoundNextTurn");

            // Hold the current state
            var CurrentRoundState = _instanceC.BattleEngine.RoundStateEnum;

            // If the round is over start a new one...
            if (CurrentRoundState == RoundEnum.NewRound)
            {
                ClearMessages();
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

            
                // output results of the game after battle is over
                var myResult = _instanceC.BattleEngine.GetResultsOutput();
                var myScore = _instanceC.BattleEngine.GetScoreValue();
                Debug.Write(myResult);

                // Let the user know the game is over
                ClearMessages();    // Clear message
                AppendMessage("Game Over\n"); // Show Game Over

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
                    await Navigation.PushAsync(new GameOver(new ScoreDetailViewModel(myScoreObject)));
                    //await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
                }
      

                return;
            }

      
            //updates the data
            _instanceC.LoadDataCommand.Execute(null);
            // game message
            GameMessage();

        }


        public void ClearMessages()
        {
            MessageText.Text = "";
            htmlSource.Html = _instanceC.BattleEngine.BattleMessages.GetHTMLBlankMessage();
            HtmlBox.Source = htmlSource;
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

            var message = _instanceC.BattleEngine.BattleMessages.TurnMessage;
            Debug.WriteLine("#####The Message###: " + message);

           // game messages
           htmlSource.Html = _instanceC.BattleEngine.BattleMessages.GetHTMLFormattedTurnMessage();
            HtmlBox.Source = HtmlBox.Source = htmlSource;

        }
    

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