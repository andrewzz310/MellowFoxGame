using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleBeginPage : ContentPage
	{
        private CharactersViewModel _cinstance; //viewmodel of character
        //private MonstersViewModel _instancem; // viewmodel of monster
        private BattleViewModel _instanceB; //viewmodel of battle with character and monster

        public BattleBeginPage ()
		{
            InitializeComponent ();
            BindingContext = _cinstance = CharactersViewModel.Instance;
            BindingContext = _instanceB = BattleViewModel.Instance; 
           // BindingContext = _cinstance = CharactersViewModel.Instance;
            //BindingContext = _instancem = MonstersViewModel.Instance;
          
        }
        private async void OnItemSelectedc(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Character;
            if (data == null)
                return;

           // await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(data)));

            // Manually deselect item.
            CharactersListView.SelectedItem = null;
        }


        private async void OnItemSelectedm(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Monster;
            if (data == null)
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect item.
            MonstersListView.SelectedItem = null;
        }
    }
}