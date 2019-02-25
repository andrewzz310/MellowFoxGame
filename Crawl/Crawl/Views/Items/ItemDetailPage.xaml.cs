using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        // Item Detail ViewModel
        private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        // The detail page of the item
        public ItemDetailPage()
        {
            InitializeComponent();

            var data = new Item();

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }

        // Edit, delete or cancel buttons for the item detail page
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEditPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDeletePage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}