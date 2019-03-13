using Crawl.ViewModels;
using Crawl.Models;

namespace Crawl.Services
{
    public static class MasterDataStore
    {
        // Holds which datastore to use.

        private static DataStoreEnum _dataStoreEnum = DataStoreEnum.Mock;

        // Returns which dtatstore to use
        public static DataStoreEnum GetDataStoreMockFlag()
        {
            return _dataStoreEnum;
        }

        // Switches the datastore values.
        // Loads the databases...
        public static void ToggleDataStore(DataStoreEnum dataStoreEnum)
        {
            switch (dataStoreEnum)
            {

                case DataStoreEnum.Mock:
                    _dataStoreEnum = DataStoreEnum.Mock;
                    //Item
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Monster
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Character
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Score
                    ScoresViewModel.Instance.SetDataStore(DataStoreEnum.Mock);

   
                    BattleViewModel.Instance.SetDataStore(DataStoreEnum.Mock);

                    break;

                case DataStoreEnum.Sql:
                default:
                    _dataStoreEnum = DataStoreEnum.Sql;
                    //Item
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    //Monster
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    //Character
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    //Score
                    ScoresViewModel.Instance.SetDataStore(DataStoreEnum.Sql);

                    BattleViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    break;
            }

            // Load the Data
            //Items
            ItemsViewModel.Instance.ForceDataRefresh();
            // Monster
            MonstersViewModel.Instance.ForceDataRefresh();
            //Character
            CharactersViewModel.Instance.ForceDataRefresh();
            //Score
            ScoresViewModel.Instance.ForceDataRefresh();

            BattleViewModel.Instance.ForceDataRefresh();
         //   BattleViewModel.Instance.ForceDataRefreshM();
        }

        // Force all modes to load data...
        public static void ForceDataRestoreAll()
        {
            //Items
            ItemsViewModel.Instance.ForceDataRefresh();
            //Monster
            MonstersViewModel.Instance.ForceDataRefresh();
            //Character
            CharactersViewModel.Instance.ForceDataRefresh();
            //Battle
           // BattleViewModel.Instance.ForceDataRefresh();
            //Score
            ScoresViewModel.Instance.ForceDataRefresh();
          
        }
    }
}
