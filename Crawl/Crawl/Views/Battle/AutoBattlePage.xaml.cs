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

        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            // Can create a new battle engine...
            var myBattleEngine = new AutoBattleEngine();

            var result = myBattleEngine.RunAutoBattle();

            if (result == false)
            {
                await DisplayAlert("Error", "No Characters Avaialbe", "OK");
                return;
            }

            
            if (myBattleEngine.GetRoundsValue() < 1)
            {
                await DisplayAlert("Error", "No Rounds Fought", "OK");
                return;
            }
          
            /*
            var myResult = myBattleEngine.GetResultsOutput();
            var myScore = myBattleEngine.GetScoreValue();
            

            var outputString = "Battle Over! Score " + myScore.ToString();
            
            var action = await DisplayActionSheet(outputString, 
                "Cancel", 
                null, 
                "View Score");
            if (action == "View Score")
            {
                await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(myBattleEngine.BattleScore)));
            }
            */
        }
    }
}