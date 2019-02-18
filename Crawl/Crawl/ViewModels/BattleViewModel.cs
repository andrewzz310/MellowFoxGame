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
        //Gets character data from data store
        public Command LoadDataCommand { get; set; }
        //new char added/deleted require refresh
        private bool _needsRefresh;


        // List of monsters to display to user
        public ObservableCollection<Monster> DatasetM { get; set; }
        public Command LoadDataCommandM { get; set; }
        private bool _needsRefreshM;

        public BattleViewModel()
        {

            Title = "Character List";
            Dataset = new ObservableCollection<Character>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //listening to user events
            #region Messages

            MessagingCenter.Subscribe<CharacterDeletePage, Character>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<CharacterNewPage, Character>(this, "AddData", async (obj, data) =>
            {
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<CharacterEditPage, Character>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsync(data);
            });



            Title = "Monster List";
            DatasetM = new ObservableCollection<Monster>();
            LoadDataCommandM = new Command(async () => await ExecuteLoadDataCommandM());

            
            #region Messages
            MessagingCenter.Subscribe<MonsterDeletePage, Monster>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsyncM(data);
            });

            MessagingCenter.Subscribe<MonsterNewPage, Monster>(this, "AddData", async (obj, data) =>
            {
                await AddAsyncM(data);
            });

            MessagingCenter.Subscribe<MonsterEditPage, Monster>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsyncM(data);
            });


            #endregion Messages

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

        public bool NeedsRefreshM()
        {
            // Implement 
            if (_needsRefreshM)
            {
                _needsRefreshM = false;
                return true;
            }
            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefreshM(bool value)
        {
            // Implement 
            _needsRefreshM = value;

        }


        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Character(true);

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
        }



        private async Task ExecuteLoadDataCommandM()
        {
            // Implement 
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetM.Clear();
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
                    DatasetM.Add(data);
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



        public void ForceDataRefreshM()
        {
            // Implement 
            // Reset
            var canExecute = LoadDataCommandM.CanExecute(null);
            LoadDataCommandM.Execute(null);
        }

        //adds new character
        public async Task<bool> AddAsync(Character data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Character(data);
            return myReturn;
        }

        //deletes character
        public async Task<bool> DeleteAsync(Character data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }

        //updates character
        public async Task<bool> UpdateAsync(Character data)
        {

            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Character(myData);

            _needsRefresh = true;

            return true;
        }




        public async Task<bool> AddAsyncM(Monster data)
        {
            // Implement
            // Implement 
            DatasetM.Add(data);
            var myReturn = await DataStore.AddAsync_Monster(data);
            return myReturn;
            //return false;
        }

        public async Task<bool> DeleteAsyncM(Monster data)
        {
            // Implement 
            // Implement 
            DatasetM.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Monster(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsyncM(Monster data)
        {
            // Implement 
            var myData = DatasetM.FirstOrDefault(arg => arg.Id == data.Id);
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
        public async Task<Character> GetAsync(string id)
        {

            var myData = await DataStore.GetAsync_Character(id);
            return myData;
        }
        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsync(Character data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Character(data);
            return myReturn;
        }


        // Call to database to ensure most recent
        public async Task<Monster> GetAsyncM(string id)
        {
            // Implement 
            var myData = await DataStore.GetAsync_Monster(id);
            return myData;

        }

        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsyncM(Monster data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Monster(data);
            return myReturn;
        }
        #endregion DataOperations
    }
}