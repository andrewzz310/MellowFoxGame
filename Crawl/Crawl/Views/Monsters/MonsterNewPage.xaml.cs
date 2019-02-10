using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.Controllers;
using Crawl.Models;

namespace Crawl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterNewPage : ContentPage
    {
        public Monster Data { get; set; }

        // Constructor for the page, will create a new black character that can tehn get updated
        public MonsterNewPage()
        {
            InitializeComponent();

            Data = new Monster
            {
                Name = "Monster name",
                Description = "This is an Monster description.",
                Id = Guid.NewGuid().ToString(),
                ExperienceTotal = 100,
                ImageURI = ItemsController.DefaultImageURIMonster
            };

            BindingContext = this;
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemsController.DefaultImageURIMonster;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}