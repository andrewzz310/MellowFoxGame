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
    public class MonstersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MonstersViewModel _instance;

        public static MonstersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonstersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Monster> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public MonstersViewModel()
        {
            Title = "Monster List";
            Dataset = new ObservableCollection<Monster>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Implement 
            // Implement 
            #region Messages
            MessagingCenter.Subscribe<MonsterDeletePage, Monster>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<MonsterNewPage, Monster>(this, "AddData", async (obj, data) =>
            {
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<MonsterEditPage, Monster>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsync(data);
            });

            #endregion Messages
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            // Implement 
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
            // Implement 
            _needsRefresh = value;

        }

        private async Task ExecuteLoadDataCommand()
        {
            // Implement 
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Monster(true);

                // Example of how to sort the database output using a linq query.
                //Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ThenByDescending(a => a.Level)
                    .ToList();

                // Then load the data structure
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
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
            return;

        }
        
        public void ForceDataRefresh()
        {
            // Implement 
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }

        #region DataOperations

        public async Task<bool> AddAsync(Monster data)
        {
            // Implement
            // Implement 
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Monster(data);
            return myReturn;
            //return false;
        }

        public async Task<bool> DeleteAsync(Monster data)
        {
            // Implement 
            // Implement 
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Monster(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Monster data)
        {
            // Implement 
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Monster(myData);

            _needsRefresh = true;

            return true;
            
        }

        // Call to database to ensure most recent
        public async Task<Monster> GetAsync(string id)
        {
            // Implement 
            var myData = await DataStore.GetAsync_Monster(id);
            return myData;
           
        }

        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsync(Monster data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Monster(data);
            return myReturn;
        }

        #endregion DataOperations

    }
}