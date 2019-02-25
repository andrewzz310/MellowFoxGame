using System;

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScoreEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ScoreDetailViewModel _viewModel;

        public Score Data { get; set; }

        // Edit page based on score detailview model saved to data 
        public ScoreEditPage(ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();
            
            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        // button for save the edited score page
        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // button for cancel
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}