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
        private CharactersViewModel _instancea;

        public SelectCharacters()
        {
            InitializeComponent();
            BindingContext = _instancea = CharactersViewModel.Instance;
        }

        private async void OnItemSelectedc(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            // show details of the character
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            SelectCharactersListView.SelectedItem = null;
        }

        // Create a character
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterNewPage());
        }

        // Once characters selected, go to the battle page
        private async void Battle_Command(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new BattleBeginPage());
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

            if (_instancea.Dataset.Count == 0)
            {
                _instancea.LoadDataCommand.Execute(null);
            }
            else if (_instancea.NeedsRefresh())
            {
                _instancea.LoadDataCommand.Execute(null);
            }

            BindingContext = _instancea;
        }
    }
}