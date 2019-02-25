using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonstersPage : ContentPage
    {
        // Monster View Model
        private MonstersViewModel _instancem;

        public MonstersPage()
        {
            InitializeComponent();
            BindingContext = _instancem = MonstersViewModel.Instance;
        }

        // Display the monster that is selected
        private async void OnItemSelectedm(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect item.
            MonstersListView.SelectedItem = null;
        }

        // Add a new monster
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterNewPage());
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

            if (_instancem.Dataset.Count == 0)
            {
                _instancem.LoadDataCommand.Execute(null);
            }
            else if (_instancem.NeedsRefresh())
            {
                _instancem.LoadDataCommand.Execute(null);
            }

            BindingContext = _instancem;
        }

    }
}