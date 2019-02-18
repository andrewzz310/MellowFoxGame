using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;
using Crawl.Controllers;
using Crawl.GameEngine;

namespace Crawl.ViewModels
{
    public class ScoresViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ScoresViewModel _instance;

        public static ScoresViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoresViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Score> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public ScoresViewModel()
        {
            Title = "Score List";
            Dataset = new ObservableCollection<Score>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            #region Messages
            MessagingCenter.Subscribe<ScoreDeletePage, Score>(this, "DeleteData", async (obj, data) =>
            {
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<ScoreNewPage, Score>(this, "AddData", async (obj, data) =>
            {
                await AddAsync(data);
            });

            MessagingCenter.Subscribe<ScoreEditPage, Score>(this, "EditData", async (obj, data) =>
            {
                await UpdateAsync(data);
            });

            #endregion Messages

            // Implement 
        }

        #region Refresh
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
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Score(true);

                // Example of how to sort the database output using a linq query.
                //Sort the list
                dataset = dataset
                    .OrderBy(a => a.ExperienceGainedTotal)
                    .ThenBy(a => a.ScoreTotal)
                    .ThenBy(a => a.GameDate)
                    .ThenByDescending(a => a.TurnCount)
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
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }

        #endregion Refresh

        #region DataOperations
        public async Task<bool> AddAsync(Score data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Score(data);
            return myReturn;
        }

        public async Task<bool> DeleteAsync(Score data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Score(data);
            return myReturn;
        }

        public async Task<bool> UpdateAsync(Score data)
        {
            // Find the Score, then update it
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Score(myData);

            _needsRefresh = true;

            return true;
        }

        // Call to database to ensure most recent
        public async Task<Score> GetAsync(string id)
        {
            var myData = await DataStore.GetAsync_Score(id);
            return myData;
        }

        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsync(Score data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Score(data);
            return myReturn;
        }

        public Score CheckIfScoreExists(Score data)
        {
            // This will walk the Scores and find if there is one that is the same.
            // If so, it returns the Score...

            var myList = Dataset.Where(a =>
                                        a.ExperienceGainedTotal == data.ExperienceGainedTotal &&
                                        a.BattleNumber == data.BattleNumber &&
                                        a.ScoreTotal == data.ScoreTotal &&
                                        a.TurnCount == data.TurnCount &&
                                        a.RoundCount == data.RoundCount &&
                                        a.MonsterSlainNumber == data.MonsterSlainNumber)
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }

        #endregion DataOperations   

        #region ScoreConversion

        // Takes an item string ID and looks it up and returns the item
        // This is because the Items on a character are stores as strings of the GUID.  That way it can be saved to the DB.
        public Score GetScore(string ScoreID)
        {
            if (string.IsNullOrEmpty(ScoreID))
            {
                return null;
            }

            Score myData = DataStore.GetAsync_Score(ScoreID).GetAwaiter().GetResult();

            if (myData == null)
            {
                return null;
            }

            return myData;
        }

        #endregion ScoreConversion
    }
}