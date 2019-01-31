<<<<<<< HEAD
﻿
using System;
using Crawl.Models;
using Crawl.Controllers;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterEditPage : ContentPage
    {
        // ReSharper disable once NotAccessedField.Local
        private CharacterDetailViewModel _viewModel;

        // The data returned from the edit.
        public Character Data { get; set; }

        // The constructor takes a View Model
        // It needs to set the Picker values after doing the bindings.
        public CharacterEditPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;

            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;

            //Need to make the SelectedItem a string, so it can select the correct item.
           // LocationPicker.SelectedItem = Data.Location.ToString();
          //  AttributePicker.SelectedItem = Data.Attribute.ToString();

        }

        // Save on the Tool bar
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemsController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        // The stepper function for Range
        void Age_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AgeValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Value
        void LevelValue_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = String.Format("{0}", e.NewValue);
        }

        
    }
}


/*
using System;
=======
﻿using System;
>>>>>>> 20f9049006559ec397faf2d9a8b59e8d57f0849c

using Crawl.Models;
using Crawl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private CharacterDetailViewModel _viewModel;

        public Character Data { get; set; }

        public CharacterEditPage(CharacterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();
            

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

	    public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
<<<<<<< HEAD
}
*/
=======
}
>>>>>>> 20f9049006559ec397faf2d9a8b59e8d57f0849c
