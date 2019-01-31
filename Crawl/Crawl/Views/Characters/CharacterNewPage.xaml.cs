using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
<<<<<<< HEAD
using Crawl.Controllers;
using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterNewPage : ContentPage
    {
        public Character Data { get; set; }

        // Constructor for the page, will create a new black character that can tehn get updated
        public CharacterNewPage()
        {
            InitializeComponent();

            Data = new Character
            {
                Name = "Character name",
                Description = "This is an Character description.",
                Id = Guid.NewGuid().ToString(),
                Age = 10,
                Level = 1,
                ImageURI = ItemsController.DefaultImageURICharacter
            };

            BindingContext = this;
            //Need to make the SelectedItem a string, so it can select the correct item.

            // commented out from item
        //    LocationPicker.SelectedItem = Data.Location.ToString();
          //  AttributePicker.SelectedItem = Data.Attribute.ToString();
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemsController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
=======
>>>>>>> 20f9049006559ec397faf2d9a8b59e8d57f0849c

using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterNewPage : ContentPage
    {
        public Character Data { get; set; }

        public CharacterNewPage()
        {
            InitializeComponent();

            Data = new Character
            {
                Name = "Character Name",
                Description = "This is a Character description.",
                Level =1,
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
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
