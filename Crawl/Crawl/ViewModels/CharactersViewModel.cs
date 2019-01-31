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
    public class CharactersViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static CharactersViewModel _instance;

        public static CharactersViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharactersViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Character> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public CharactersViewModel()
        {

            Title = "Character List";
            Dataset = new ObservableCollection<Character>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            // Implement 
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
                    .ThenBy(a => a.Age)
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

        public void ForceDataRefresh()
        {
            // Implement 
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }

       

        public async Task<bool> AddAsync(Character data)
        {
            // Implement 
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> DeleteAsync(Character data)
        {
            // Implement 
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Character(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Character data)
        {
            // Implement 
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

        // Call to database to ensure most recent
        public async Task<Character> GetAsync(string id)
        {
            // Implement 
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
        /*
        public Item CheckIfItemExists(Character data)
        {
            // This will walk the items and find if there is one that is the same.
            // If so, it returns the item...

            var myList = Dataset.Where(a =>
                                        
                                        a.Name == data.Name &&
                                        a.Description == data.Description &&
                                        a.Age == data.Age &&
                                       
                                        a.Level == data.Level)
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }
        #endregion DataOperations
        */




    }
}