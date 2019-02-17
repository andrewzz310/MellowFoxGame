using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>(); //Holds mock data for Items
        private List<Character> _characterDataset = new List<Character>(); //Holds mock data for Characters
        private List<Monster> _monsterDataset = new List<Monster>(); //Holds mock data for Monsters
        private List<Score> _scoreDataset = new List<Score>(); //Holds mock data for Score

        private MockDataStore()
        {
            InitilizeSeedData();
        }

        //loads mock data
        private void InitilizeSeedData()
        {

            // Load Items
            _itemDataset.Add(new Item("Shuriken", "This is a Shuriken  Item", "shuriken.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            _itemDataset.Add(new Item("Armor", "This is a Armor Item", "armors.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            _itemDataset.Add(new Item("Ring of Power", "This is a Ring of Power Item", "ringofpower.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            _itemDataset.Add(new Item("Two-Sided Hammer", "This is a Two-Sided Hammer Item", "hammer1.png", 2, 5, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            _itemDataset.Add(new Item("Bow and Arrow", "This is a Bow and Arrow Item", "bowandarrows.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            _itemDataset.Add(new Item("Turbo", "This is a Turbo Item", "turbo.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Speed));
            _itemDataset.Add(new Item("Staff Sword", "This is a Staff Sword Item", "sword.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            _itemDataset.Add(new Item("Potion", "This is a Potion Item", "potion.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.CurrentHealth));


            // Characters initialized with level 2, xp 300, etc based on ppt deck
            _characterDataset.Add(new Character("Elf", "Special power is bow and arrow item", "elf.png",  2, 300, 1,2,1, PreferredItemEnum.Armor));
            _characterDataset.Add(new Character("Dwarf", "Special power is hammer item", "Dwarf.png",  2, 300,1,2,1, PreferredItemEnum.BowArrow));
            _characterDataset.Add(new Character("Magician", "Special power is staff item", "magician.png",  2, 300,1,2,1, PreferredItemEnum.RingOfPower));
            _characterDataset.Add(new Character("Knight", "Special power is sword item", "Knight.png",  2, 300,1,2,1, PreferredItemEnum.Shuriken));
            _characterDataset.Add(new Character("Ninja", "Special power is damage without any items", "ninja.png",  2, 300,1,2,1, PreferredItemEnum.Staff));
            _characterDataset.Add(new Character("Mellow Fox", "Special power is automatically skips level ", "fox.png",  2, 300,1,2,1, PreferredItemEnum.Sword));


            // Monsters initialized 
            _monsterDataset.Add(new Monster("Dragon", "This is a Dragon monster", "dragon1.png", PreferredItemEnum.Armor));
            _monsterDataset.Add(new Monster("Ork", "This is a Ork monster", "ork.png",PreferredItemEnum.BowArrow));
            _monsterDataset.Add(new Monster("Ogre", "This is a Ogre monster", "ogre.png", PreferredItemEnum.RingOfPower));
            _monsterDataset.Add(new Monster("100 Handed Giant", "This is a 100 handed giant monster", "100giant.png", PreferredItemEnum.Shuriken));
            _monsterDataset.Add(new Monster("Zombie", "This is a Zombie monster", "zombie.png", PreferredItemEnum.Staff));
            _monsterDataset.Add(new Monster("Hellraiser", "This is a Hellraiser monster", "hellraiser.png", PreferredItemEnum.Sword));

            // Implement Scores
        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            // Implement
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            //Items
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            //Monster
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            //Character
            CharactersViewModel.Instance.SetNeedsRefresh(true);
           
            // Implement Scores
        }

        public void InitializeDatabaseNewTables()
        {
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        #region Item
        // Item

        public List<Item> GetAllItems()
        {
            List<Item> myList = _itemDataset;
            return myList;
        }
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                _itemDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                await AddAsync_Item(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }

        #endregion Item

        #region Character
        // Character
        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                _characterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }


        public async Task<bool> AddAsync_Character(Character data)
        {
            _characterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }
            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }

        #endregion Character

        #region Monster
        //Monster

        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Monster(data.Id);
            if (oldData == null)
            {
                _monsterDataset.Add(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Monster(data);
            if (UpdateResult)
            {
                await AddAsync_Monster(data);
                return true;
            }

            return false;
        }
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(_monsterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterDataset);
        }

        #endregion Monster

        #region Score
        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            // Implement
            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            // Implement
            return false;
        }

            public async Task<Score> GetAsync_Score(string id)
        {
            // Implement
            return null;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            // Implement
            return null;
        }
        #endregion Score
    }
}