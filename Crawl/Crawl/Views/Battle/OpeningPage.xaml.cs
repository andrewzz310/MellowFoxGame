using System;

using Crawl.Views;   

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Views.Battle;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OpeningPage : ContentPage
	{
		public OpeningPage ()
		{
			InitializeComponent ();
		}

        // auto battle page
        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoBattlePage());
        }

        // This is the Battle button that is selected which starts by player choosing characters
        private async void ManualBattleButton_Command(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SelectCharacters());
            await Navigation.PushAsync(new BattleCharacterSelectPage());
        }

        
        // go to characters page
        private async void Characters_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharactersPage());
        }

        // go to Monsters page
        private async void Monsters_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonstersPage());
        }
        //go to items page
        private async void Items_Command (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        private async void Scores_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoresPage());
        }
    }
}