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

        private async void AutoBattleButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoBattlePage());
        }

        private async void ManualBattleButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutoBattlePage());
        }

        /*
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
        */
    }
}