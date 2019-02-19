using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;

namespace Crawl.ViewModels
{
    public class BattleViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static BattleViewModel _instance;

        public static BattleViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleViewModel();
                }
                return _instance;
            }
        }

        //List of characters displayed to user
        public ObservableCollection<Character> Dataset { get; set; }
        public ObservableCollection<Monster> DatasetMons { get; set; }
        //Gets character data from data store
        public Command LoadDataCommand { get; set; }

        //new char added/deleted require refresh
        private bool _needsRefresh;

        public BattleViewModel()
        {

            Title = "Characters"; //Not showing up on the screen
            Dataset = new ObservableCollection<Character>();
            DatasetMons = new ObservableCollection<Monster>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {

            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Character
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Character(true);

                //Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ThenByDescending(a => a.Level)
                    .ToList();

                // Then load the data structure
                foreach (var data_char in dataset)
                {
                    Dataset.Add(data_char);
                }
              
                //Monsters
                DatasetMons.Clear();
                var dataset_mons = await DataStore.GetAllAsync_Monster(true);

                //Sort the list
                dataset_mons = dataset_mons
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ThenByDescending(a => a.Level)
                    .ToList();

                // Then load the data structure
                foreach (var data_mons in dataset_mons)
                {
                    DatasetMons.Add(data_mons);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }

        public void ForceDataRefresh()
        {

          var canExecute = LoadDataCommand.CanExecute(null);
          LoadDataCommand.Execute(null);
        }
    }
}