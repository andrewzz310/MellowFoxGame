using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Views.Battle;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCharacters : ContentPage
    {
        // charactersviewmodel for binding
        private BattleViewModel _instanceC;

        public SelectCharacters()
        {
            InitializeComponent();
            BindingContext = _instanceC = BattleViewModel.Instance;
        }

        private async void OnItemSelectedc(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            // show details of the character
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharactersBattle.SelectedItem = null;
        }

        // Create a character
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterNewPage());
        }

        // Once characters selected, go to the battle page
        private async void Battle_Command(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new BattleBeginPage(_instanceC));
        }

        // cancel the selected characters by refreshing a new select characters page
        private async void Cancel_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectCharacters());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_instanceC.DatasetChars.Count == 0)
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