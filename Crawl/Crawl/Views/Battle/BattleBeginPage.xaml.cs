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
using Crawl.GameEngine;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleBeginPage : ContentPage
	{

        //viewmodel of battle with character and monster data for binding
        //private BattleViewModel _instance;
        private BattleViewModel _instanceC;

        BattleEngine a = new BattleEngine();
        //  think about passing characters or something else instead of game engines
        public BattleBeginPage ()
		{

            InitializeComponent();
            BindingContext = _instanceC = BattleViewModel.Instance;

        }

        public BattleBeginPage(BattleViewModel a)
        {
            InitializeComponent();

            _instanceC = a;
        }
        
        // For now use this to begin the battle for testing purposes
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
           // var myBattleEngine = new BattleEngine();
            // picks 6 characters
            a.AddCharactersToBattle();
            //init console message and characters
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("The Mellow Fox Game Battle has Started" + " Total Characters :" + a.CharacterList.Count);
            Debug.WriteLine("Characters are Below :");
            
            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(a.CharacterList[i].FormatOutput());
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);


            string outputString = "Battle Has added these characters" +
          a.CharacterList[0].FormatOutput() + Environment.NewLine +
          a.CharacterList[1].FormatOutput() + "sadf";




            // the pop up for either cancel or see the score details
            var action = await DisplayActionSheet(outputString,
                "Cancel",
                null,
                "View Score Results");


            /*
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            //do something for click like attack or something
           await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharactersBattle.SelectedItem = null;
            */
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
            await Navigation.PushAsync(new RoundOver(a.CharacterList[0]));
        }

        // when game over, display game over page
        private async void GameOver_Command(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameOver());
        }


        bool hasAppearedOnce = false;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;


            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_instanceC.DatasetChars.Count == 0 || _instanceC.DatasetMons.Count == 0 || 
                _instanceC.DatasetItems.Count == 0)
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