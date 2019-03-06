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
        // BattleviewModel for binding
        private BattleViewModel _instanceC;

        public SelectCharacters()
        {
            InitializeComponent();
            BindingContext = _instanceC = BattleViewModel.Instance;
            //clear character list
            BattleViewModel.Instance.ClearCharacterLists();
            // Start with Next button disabled
            NextButton.IsEnabled = false;
        }

        // Close this page
        async void OnNextClicked(object sender, EventArgs args)
        {

            // Jump to Main Battle Page
            await Navigation.PushAsync(new BattleBeginPage(_instanceC));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // This shows available characters based on characterlist page which can also be modified
        private async void OnAvailableCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            // Manually deselect item.
            CharactersBattle.SelectedItem = null;

            var currentCount = _instanceC.SelectedCharacters.Count();
            // Don't add more than the party max
            if (currentCount < GameGlobals.MaxNumberPartyPlayers)
            {
                MessagingCenter.Send(this, "AddSelectedCharacter", data);
            }

            // refresh the count
            currentCount = _instanceC.SelectedCharacters.Count();

            // Set the Button to be enabled or disabled if no characters in the party
            NextButton.IsEnabled = true;
            if (currentCount == 0)
            {
                NextButton.IsEnabled = false;
            }
            // add the current count of total characters
            PartyCountLabel.Text = currentCount.ToString();
        }

        // this shows the characters that are selected from the characterlist page
        private async void OnSelectedCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
            {
                return;
            }

            // Manually deselect item.
            SelectedCharactersBattle.SelectedItem = null;

            MessagingCenter.Send(this, "RemoveSelectedCharacter", data);

            // If no characters disable Next button
            var currentCount = _instanceC.SelectedCharacters.Count();
            if (currentCount == 0)
            {
                NextButton.IsEnabled = false;
            }

            PartyCountLabel.Text = currentCount.ToString();
        }


        // Create a character
        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterNewPage());
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