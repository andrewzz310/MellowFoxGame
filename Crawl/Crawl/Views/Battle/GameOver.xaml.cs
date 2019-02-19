using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;


namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameOver : ContentPage
	{
        // scoredetailviewmodel to display scores at end of game
        private ScoreDetailViewModel _viewModel;

		public GameOver (ScoreDetailViewModel viewModel)
		{
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public GameOver()
        {
            InitializeComponent();

            var data = new Score();

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
        }

        // go back home page
        private async void HomeButton_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpeningPage());
        }


    }
}