using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crawl.GameEngine;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		public AutoBattlePage ()
		{
			InitializeComponent ();
		}

        // Auto Battle Starts here when this button is clicked
        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            // Can create a new battle engine...
            var myBattleEngine = new AutoBattleEngine();

            // run auto battle
            var result = myBattleEngine.RunAutoBattle();

            if (result == false)
            {
                await DisplayAlert("Error", "No Characters Available", "OK");
                return;
            }

            if (myBattleEngine.GetRoundsValue() < 1)
            {
                await DisplayAlert("Error", "No Rounds Fought", "OK");
                return;
            }
          
            // output results of the game after battle is over
            var myResult = myBattleEngine.GetResultsOutput();
            var myScore = myBattleEngine.GetScoreValue();

            
            
            var outputString = "Mellow Fox Battle Over!";
            
            // the pop up for either cancel or see the score details
            var action = await DisplayActionSheet(outputString, 
                "Cancel", 
                null, 
                "View Score Results");
            
            // Show the results
            if (action == "View Score Results")
            {
                var myScoreObject = myBattleEngine.GetScoreObject();
                await Navigation.PushAsync(new GameOver(new ScoreDetailViewModel(myScoreObject)));
            }
            
            
        }
    }
}