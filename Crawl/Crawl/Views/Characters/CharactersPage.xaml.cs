using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharactersPage : ContentPage
    {
        private CharactersViewModel _instance;

        public CharactersPage()
        {
            InitializeComponent();
            BindingContext = _instance = CharactersViewModel.Instance;
        }

        private async void OnItemSelectedc(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharactersListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterNewPage());
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

            if (_instance.Dataset.Count == 0)
            {
                _instance.LoadDataCommand.Execute(null);
            }
            else if (_instance.NeedsRefresh())
            {
                _instance.LoadDataCommand.Execute(null);
            }

            BindingContext = _instance;
        }
    }
}