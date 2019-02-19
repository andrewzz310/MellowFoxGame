using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Services;

namespace Crawl.Services
{
    public sealed class SQLDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {
            // Implement
            CreateTables();
            InitializeDatabaseNewTables();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<BaseCharacter>().Wait();
            App.Database.DropTableAsync<BaseMonster>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        public void InitializeDatabaseNewTables()
        {
            //Delete the tables
            DeleteTables();

            // Make them again
            CreateTables();

            // Populate them
            InitializeSeedData();

            // Tell view Models they need to refresh
            NotifyViewModelsOfDataChange();

        }

        private async void InitializeSeedData()
        {
            // Add Default SQL DB Items
            await AddAsync_Item(new Item("Shuriken", "This is a Shuriken  Item", "shuriken.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Armor", "This is a Armor Item", "armors.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("Ring of Power", "This is a Ring of Power Item", "ringofpower.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("Two-Sided Hammer", "This is a Two-Sided Hammer Item", "hammer1.png", 2, 5, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Bow and Arrow", "This is a Bow and Arrow Item", "bowandarrows.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Turbo", "This is a Turbo Item", "turbo.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Speed));
            await AddAsync_Item(new Item("Staff Sword", "This is a Staff Sword Item", "sword.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Potion", "This is a Potion Item", "potion.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.CurrentHealth));

            // Add Default SQL DB Characters
            await AddAsync_Character(new Character("Elf", "Special power is bow and arrow item", "elf.png", 2, 300, 1, 2, 1, PreferredItemEnum.Armor));
            await AddAsync_Character(new Character("Magician", "Special power is staff item", "magician.png", 2, 300, 1, 2, 1, PreferredItemEnum.RingOfPower));
            await AddAsync_Character(new Character("Knight", "Special power is sword item", "Knight.png", 2, 300, 1, 2, 1, PreferredItemEnum.Shuriken));
            await AddAsync_Character(new Character("Ninja", "Special power is damage without any items", "ninja.png", 2, 300, 1, 2, 1, PreferredItemEnum.Staff));
            await AddAsync_Character(new Character("Mellow Fox", "Special power is automatically skips level ", "fox.png", 2, 300, 1, 2, 1, PreferredItemEnum.Sword));

            // Add Default Default SQL Monsters
            await AddAsync_Monster(new Monster("Dragon", "This is a Dragon monster", "dragon1.png", PreferredItemEnum.Armor));
            await AddAsync_Monster(new Monster("Ork", "This is a Ork monster", "ork.png", PreferredItemEnum.BowArrow));
            await AddAsync_Monster(new Monster("Ogre", "This is a Ogre monster", "ogre.png", PreferredItemEnum.RingOfPower));
            await AddAsync_Monster(new Monster("100 Handed Giant", "This is a 100 handed giant monster", "100giant.png", PreferredItemEnum.Shuriken));
            await AddAsync_Monster(new Monster("Zombie", "This is a Zombie monster", "zombie.png", PreferredItemEnum.Staff));
            await AddAsync_Monster(new Monster("Hellraiser", "This is a Hellraiser monster", "hellraiser.png", PreferredItemEnum.Sword));

            // Add Default SQL DB Score
            await AddAsync_Score(new Score(battleNumber: 1, scoreTotal: 2, gameDate: DateTime.Now, autoBattle: true, turnCount: 3, roundCount: 4, monsterSlainNumber: 5, experienceGainedTotal: 6, characterAtDeathList: "Elf", monstersKilledList: "Dragon", itemsDroppedList: "Bow and Arrow"));
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLSecond Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLThird Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLFourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLFifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLSixth Score", ScoreTotal = 666 });

        }

        #region Item
        // Item

        // Add InsertUpdateAsync_Item Method

        // Check to see if the item exists
        // Add your code here.

        // If it does not exist, then Insert it into the DB
        // Add your code here.
        // return true;

        // If it does exist, Update it into the DB
        // Add your code here
        // return true;

        // If you got to here then return false;

        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            // Implement

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            var result = await App.Database.GetAsync<Item>(id);
            return result;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;
        }
        #endregion Item

        #region Character
        // Character
        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {
            // Implement

            return false;
        }

        // Conver to BaseCharacter and then add it
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Update character data
            data.Update(data);

            // Convert to BaseCharacter type to add to DB
            var databaseChar = new BaseCharacter(data);

            // Insert BaseCharacter
            var result = await App.Database.InsertAsync(databaseChar);

            // If inserstion is successful, return true
            if (result == 1)
                return true;
            return false;
        }

        // Convert to BaseCharacter and then update it
        public async Task<bool> UpdateAsync_Character(Character data)
        {

            // Update character data
            data.Update(data);

            // Convert Character data to BaseCharacter data
            var castData = new BaseCharacter(data);

            // Update BaseCharacter data in DB
            var result = await App.Database.UpdateAsync(castData);

            // If update was successful, returen true
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the character and convert to Character to then delete it
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            // Convert Monster to BaseMonster
            var castData = new BaseCharacter(data);

            // Delete BaseMonster from DB
            var result = await App.Database.DeleteAsync(data);

            // If delete was successful, return true
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Get the Character Base, and Load it back as Character
        public async Task<Character> GetAsync_Character(string id)
        {
            // Get BaseCharacter from BaseCharacter table
            var result = await App.Database.GetAsync<BaseCharacter>(id);

            // Convert BaseCharacter to Character
            var data = new Character(result);

            return data;
        }

        // Load each character as the base character, 
        // Then then convert the list to characters to push up to the view model
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            // Get list of BaseCharacters from BaseCharacter table
            var result = await App.Database.Table<BaseCharacter>().ToListAsync();

            // Convert BaseCharacter list to Characters
            var ret = result.Select(c => new Character(c)).ToList();

            return ret;
        }

        #endregion Character

        #region Monster
        //Monster

        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {
            // Implement

            return false;
        }

        public async Task<bool> AddAsync_Monster(Monster data)
        {
            // Update Monster Data
            data.Update(data);

            // Convert Monster to BaseMonster
            var castData = new BaseMonster(data);

            // Add Monster to DB
            var result = await App.Database.InsertAsync(castData);

            // If insertion was successful, return true;
            if (result == 1)
                return true;
            return false;
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Update Monster Data
            data.Update(data);

            // Convert Monster data to BaseMonster data
            var castData = new BaseMonster(data);

            // Update BaseMonster data in DB
            var result = await App.Database.UpdateAsync(castData);

            // If update was successful, return true
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Convert Monster data to BaseMonster data
            var castData = new BaseMonster(data);

            // Delete BaseMonster from DB
            var result = await App.Database.DeleteAsync(castData);

            // If deletion is successful, return true
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            // Get BaseMonster from BaseMonster table
            var result = await App.Database.GetAsync<BaseMonster>(id);

            // Convert BaseMonster to Monster
            var ret = new Monster(result);
            return ret;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            // Get list of BaseMonsters from BaseMonster table.
            var result = await App.Database.Table<BaseMonster>().ToListAsync();

            // Convert list of BaseMonsters to Monsters
            var ret = result.Select(m => new Monster(m)).ToList();
            return ret;
        }

        #endregion Monster


        public async Task<bool> InsertUpdateAsync_Score(Score data)
        {
            var oldData = await GetAsync_Score(data.Id);
            if (oldData == null)
            {
                await AddAsync_Score(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Score(data);
            if (UpdateResult)
            {
                await AddAsync_Score(data);
                return true;
            }

            return false;
        }

        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);
            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;

        }
    }
}