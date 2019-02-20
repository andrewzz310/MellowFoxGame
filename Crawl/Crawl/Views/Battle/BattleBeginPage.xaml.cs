using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Views;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleBeginPage : ContentPage
	{
        //viewmodel of battle with character and monster data for binding
       private BattleViewModel _instance;

        public BattleBeginPage ()
		{

            InitializeComponent();
            BindingContext = _instance = BattleViewModel.Instance;

        }
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            //do something for click like attack or something
           await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharactersBattle.SelectedItem = null;
        }

        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            //do something for click like attack or something
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            //Manually deselect item.
            MonstersBattle.SelectedItem = null;
        }


        //When message says round is over, display round over page
        private async void RoundOver_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoundOver());
        }


        bool hasAppearedOnce = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            //#region HorizontalListView
            //if (!hasAppearedOnce)
            //{

            //    hasAppearedOnce = true;
            //    var padding = (BattleScene.Width - MessagesListView.Height) / 2;

            //    MessagesListView.HeightRequest = MessagesLayoutFrame.Width;
            //    MessagesLayoutFrameInner.WidthRequest = MessagesLayoutFrame.Width;
            //    MessagesLayoutFrameInner.Padding = new Thickness(0);
            //    MessagesLayoutFrame.Padding = new Thickness(0);
            //    MessagesLayoutFrame.IsClippedToBounds = true;
            //    Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(MessagesLayoutFrameInner, new Rectangle(0, 0 - padding, AbsoluteLayout.AutoSize, MessagesListView.Height - padding));
            //    MessagesLayoutFrameInner.IsClippedToBounds = true;
            //    #endregion


            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_instance.DatasetChars.Count == 0 || _instance.DatasetMons.Count == 0 || 
                _instance.DatasetItems.Count == 0)
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