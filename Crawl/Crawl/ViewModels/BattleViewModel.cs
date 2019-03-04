using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Crawl.Models;
using Crawl.Views;
using System.Linq;
using Crawl.GameEngine;
using Crawl.Views.Battle;

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
        public ObservableCollection<Character> DatasetChars { get; set; }
        //List of monsters displayed to user
        public ObservableCollection<Monster> DatasetMons { get; set; }
        //List of Items displayed to user
        public ObservableCollection<Item> DatasetItems { get; set; }

        //Class for Selected Characters
        public ObservableCollection<Character> SelectedCharacters { get; set; }

        // Class for the AvailableCharacters
        public ObservableCollection<Character> AvailableCharacters { get; set; }


        //Gets data all ListViews (3x) from data store
        public Command LoadDataCommand { get; set; }

        // Make a copy of the BattleEngine
        public BattleEngine BattleEngine;

        //new char added/deleted require refresh
        private bool _needsRefresh;

        public BattleViewModel()
        {

            //Title = "Characters"; //Not showing up on the screen
            DatasetChars = new ObservableCollection<Character>();
            DatasetMons = new ObservableCollection<Monster>();
            DatasetItems = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            Title = "Battle";

            SelectedCharacters = new ObservableCollection<Character>();
            AvailableCharacters = new ObservableCollection<Character>();

            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            BattleEngine = new BattleEngine();

            // Load Data
            ExecuteLoadDataCommand().GetAwaiter().GetResult();

            MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "AddSelectedCharacter", async (obj, data) =>
            {
                SelectedListAdd(data);
            });

            MessagingCenter.Subscribe<BattleCharacterSelectPage, Character>(this, "RemoveSelectedCharacter", async (obj, data) =>
            {
                SelectedListRemove(data);
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "StartBattle", async (obj) =>
            {
                StartBattle();
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "EndBattle", async (obj) =>
            {
                EndBattle();
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "StartRound", async (obj) =>
            {
                StartRound();
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "LoadCharacters", async (obj) =>
            {
                LoadCharacters();
            });


            MessagingCenter.Subscribe<BattleMainPage>(this, "RoundNextTurn", async (obj) =>
            {
                RoundNextTurn();
            });

            MessagingCenter.Subscribe<BattleMainPage>(this, "NewRound", async (obj) =>
            {
                NewRound();
            });

        }
        public void StartBattle()
        {
            BattleViewModel.Instance.BattleEngine.StartBattle(false);
        }

        /// <summary>
        /// Call to the Engine to End the Battle
        /// </summary>
        public void EndBattle()
        {
            BattleViewModel.Instance.BattleEngine.EndBattle();
        }

        /// <summary>
        /// Call to the Engine to Start the First Round
        /// </summary>
        public void StartRound()
        {
            BattleViewModel.Instance.BattleEngine.StartRound();
        }

        /// <summary>
        /// Load the Characters from the Selected List into the Battle Engine
        /// Making a copy of the character.
        /// </summary>
        public void LoadCharacters()
        {
            foreach (var data in SelectedCharacters)
            {
                BattleViewModel.Instance.BattleEngine.CharacterList.Add(new Character(data));
            }

        }

        /// <summary>
        /// Call to the engine for the NextRound to Happen
        /// </summary>
        public void RoundNextTurn()
        {
            BattleViewModel.Instance.BattleEngine.RoundNextTurn();
        }

        /// <summary>
        /// Call to the Engine for a New Round to Happen
        /// </summary>
        public void NewRound()
        {
            BattleViewModel.Instance.BattleEngine.NewRound();
        }

        #region DataOperations
        // Call to database operation for delete
        public bool SelectedListRemove(Character data)
        {
            SelectedCharacters.Remove(data);
            return true;
        }

        // Call to database operation for add
        public bool SelectedListAdd(Character data)
        {
            SelectedCharacters.Add(data);
            return true;
        }

        // Call to database to ensure most recent
        public Character Get(string id)
        {
            var myData = SelectedCharacters.FirstOrDefault(arg => arg.Id == id);
            if (myData == null)
            {
                return null;
            }

            return myData;

        }
        #endregion DataOperations




        public void ClearCharacterLists()
        {
            AvailableCharacters.Clear();
            SelectedCharacters.Clear();

            EexecutingLoadDataCommand();
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
                DatasetChars.Clear();
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
                    DatasetChars.Add(data_char);
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

                //Items
                DatasetItems.Clear();
                var dataset_items = await DataStore.GetAllAsync_Item(true);

                //Sort the list
                dataset_items = dataset_items
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Description)
                    .ThenByDescending(a => a.Value)
                    .ToList();

                // Then load the data structure
                foreach (var data_items in dataset_items)
                {
                    DatasetItems.Add(data_items);
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

        private async Task EexecutingLoadDataCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                // SelectedCharacters, no need to change them.

                // Reload the Character List from the Character View Moel
                AvailableCharacters.Clear();
                var availableCharacters = CharactersViewModel.Instance.Dataset;
               foreach (var data in availableCharacters)
                {
                    AvailableCharacters.Add(data);
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