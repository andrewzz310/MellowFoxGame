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
           // InitializeDatabaseNewTables();
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



        private async void InitializeSeedData()
        {

            //items
            await AddAsync_Item(new Item("SQLShuriken", "This is a Shuriken  Item", "shuriken.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("SQLArmor", "This is a Armor Item", "armors.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("SQLRing of Power", "This is a Ring of Power Item", "ringofpower.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("SQLTwo-Sided Hammer", "This is a Two-Sided Hammer Item", "hammer1.png", 2, 5, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            await AddAsync_Item(new Item("SQLBow and Arrow", "This is a Bow and Arrow Item", "bowandarrows.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("SQLTurbo", "This is a Turbo Item", "turbo.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Speed));
            await AddAsync_Item(new Item("SQLStaff Sword", "This is a Staff Sword Item", "sword.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("SQLPotion", "This is a Potion Item", "potion.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.CurrentHealth));


            //characters
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock First Character", Description = "This is an Character description.", Level = 1, ImageURI = "ninja.png" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock Second Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock Third Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock Fourth Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock Fifth Character", Description = "This is an Character description.", Level = 3 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock Sixth Character", Description = "This is an Character description.", Level = 3 });

            // Monsters
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock First Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock Second Monster", Description = "This is an Monster description.", ImageURI = "100giant.png" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock Third Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock Fourth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock Fifth Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock Sixth Monster", Description = "This is an Monster description." });

            //scores
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  First Score", ScoreTotal = 111 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Second Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Third Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Fourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Fifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Sixth Score", ScoreTotal = 666 });

            //// Add Default SQL DB Items
            //await AddAsync_Item(new Item("SQLShuriken", "This is a Shuriken  Item", "shuriken.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            //await AddAsync_Item(new Item("SQLArmor", "This is a Armor Item", "armors.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            //await AddAsync_Item(new Item("SQLRing of Power", "This is a Ring of Power Item", "ringofpower.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            //await AddAsync_Item(new Item("SQLTwo-Sided Hammer", "This is a Two-Sided Hammer Item", "hammer1.png", 2, 5, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            //await AddAsync_Item(new Item("SQLBow and Arrow", "This is a Bow and Arrow Item", "bowandarrows.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            //await AddAsync_Item(new Item("SQLTurbo", "This is a Turbo Item", "turbo.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Speed));
            //await AddAsync_Item(new Item("SQLStaff Sword", "This is a Staff Sword Item", "sword.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            //await AddAsync_Item(new Item("SQLPotion", "This is a Potion Item", "potion.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.CurrentHealth));

            //// Add Default SQL DB Characters
            //await AddAsync_Character(new Character("SQLElf", "Special power is bow and arrow item", "elf.png", 2, 300, 1, 2, 1, PreferredItemEnum.Armor));
            //await AddAsync_Character(new Character("SQLMagician", "Special power is staff item", "magician.png", 2, 300, 1, 2, 1, PreferredItemEnum.RingOfPower));
            //await AddAsync_Character(new Character("SQLKnight", "Special power is sword item", "Knight.png", 2, 300, 1, 2, 1, PreferredItemEnum.Shuriken));
            //await AddAsync_Character(new Character("SQLNinja", "Special power is damage without any items", "ninja.png", 2, 300, 1, 2, 1, PreferredItemEnum.Staff));
            //await AddAsync_Character(new Character("SQLMellow Fox", "Special power is automatically skips level ", "fox.png", 2, 300, 1, 2, 1, PreferredItemEnum.Sword));

            //// Add Default Default SQL Monsters
            //await AddAsync_Monster(new Monster("SQLDragon", "This is a Dragon monster", "dragon1.png", PreferredItemEnum.Armor));
            //await AddAsync_Monster(new Monster("SQLOrk", "This is a Ork monster", "ork.png", PreferredItemEnum.BowArrow));
            //await AddAsync_Monster(new Monster("SQLOgre", "This is a Ogre monster", "ogre.png", PreferredItemEnum.RingOfPower));
            //await AddAsync_Monster(new Monster("SQL100 Handed Giant", "This is a 100 handed giant monster", "100giant.png", PreferredItemEnum.Shuriken));
            //await AddAsync_Monster(new Monster("SQLZombie", "This is a Zombie monster", "zombie.png", PreferredItemEnum.Staff));
            //await AddAsync_Monster(new Monster("SQLHellraiser", "This is a Hellraiser monster", "hellraiser.png", PreferredItemEnum.Sword));

            //// Add Default SQL DB Score
            //await AddAsync_Score(new Score(battleNumber: 1, scoreTotal: 2, gameDate: DateTime.Now, autoBattle: true, turnCount: 3, roundCount: 4, monsterSlainNumber: 5, experienceGainedTotal: 6, characterAtDeathList: "Elf", monstersKilledList: "Dragon", itemsDroppedList: "Bow and Arrow"));
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLSecond Score", ScoreTotal = 222 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLThird Score", ScoreTotal = 333 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLFourth Score", ScoreTotal = 444 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLFifth Score", ScoreTotal = 555 });
            //await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLSixth Score", ScoreTotal = 666 });

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

            // Check to see if the item exist

            var oldData = await GetAsync_Item(data.Id);

            if (oldData == null)

            {

                await AddAsync_Item(data);

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
            var tempResult = await App.Database.GetAsync<Item>(id);

            var result = new Item(tempResult);

            return result;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<Item>().ToListAsync();

            var result = new List<Item>();
            foreach (var item in tempResult)
            {
                result.Add(new Item(item));
            }

            return result;
        }
        #endregion Item

        #region Character
        // Character

        // Conver to BaseCharacter and then add it
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }
    

        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist

            var oldData = await GetAsync_Character(data.Id);

            if (oldData == null)

            {

                await AddAsync_Character(data);

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


        // Convert to BaseCharacter and then update it
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the character and convert to Character to then delete it
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.DeleteAsync(dataBase);
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
            var tempResult = await App.Database.Table<BaseCharacter>().ToListAsync();

            var result = new List<Character>();
            foreach (var item in tempResult)
            {
                result.Add(new Character(item));
            }

            return result;
        }

        #endregion Character

        #region Monster
        //Monster


        public async Task<bool> AddAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            // Check to see if the item exist

            var oldData = await GetAsync_Monster(data.Id);

            if (oldData == null)

            {

                await AddAsync_Monster(data);

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


        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.DeleteAsync(dataBase);
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
            var tempResult = await App.Database.GetAsync<Score>(id);

            var result = new Score(tempResult);

            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<Score>().ToListAsync();

            var result = new List<Score>();
            foreach (var Score in tempResult)
            {
                result.Add(new Score(Score));
            }

            return result;

        }

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



    }
}