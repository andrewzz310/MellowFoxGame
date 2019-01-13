using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MellowFoxGame : ContentPage
	{
		public MellowFoxGame ()
		{
			InitializeComponent ();

		}

        //Button for ScorePage
        async void ButtonScore (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScorePage());
        }


        //Button for CharacterPage
        async void ButtonCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPage());
        }
    }
}