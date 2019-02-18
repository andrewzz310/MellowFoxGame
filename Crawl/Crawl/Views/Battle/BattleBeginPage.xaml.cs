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
        //viewmodel of battle with character and monster
        private BattleViewModel _instanceB;
       public ObservableCollection<Character> DataC { get; set; }


        public BattleBeginPage ()
		{
          
            BindingContext = _instanceB = BattleViewModel.Instance;
            
            InitializeComponent();
            DataC = _instanceB.Dataset;

            // BindingContext = _cinstance = CharactersViewModel.Instance;
            //BindingContext = _instancem = MonstersViewModel.Instance;

        }
        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

            //do something for click like attack or something
           await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            //CharactersListView.SelectedItem = null;
        }


        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            //do something for click like attack or something
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect item.
           // MonstersListView.SelectedItem = null;
        }
            
    }
}