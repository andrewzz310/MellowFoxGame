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
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Monster
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Character
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Score
                    ScoresViewModel.Instance.SetDataStore(DataStoreEnum.Mock);


                   // BattleViewModel.Instance.SetDataStore(DataStoreEnum.Mock);

                    break;

                case DataStoreEnum.Sql:
                default:
                    _dataStoreEnum = DataStoreEnum.Sql;
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    // Implement Monster
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    // Implement Character
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    // Implement Score
                    ScoresViewModel.Instance.SetDataStore(DataStoreEnum.Sql);

                   // BattleViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    break;
            }

            // Load the Data
            ItemsViewModel.Instance.ForceDataRefresh();
            // Implement Monster
            MonstersViewModel.Instance.ForceDataRefresh();
            // Implement Character
            CharactersViewModel.Instance.ForceDataRefresh();
            // Implement Score

            BattleViewModel.Instance.ForceDataRefresh();
        }

        // Force all modes to load data...
        public static void ForceDataRestoreAll()
        {
            ItemsViewModel.Instance.ForceDataRefresh();
            // Implement Monster
            MonstersViewModel.Instance.ForceDataRefresh();
            // Implement Character
           CharactersViewModel.Instance.ForceDataRefresh();
            // Implement Score

            BattleViewModel.Instance.ForceDataRefresh();
        }
    }
}
