using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crawl.Models;
using Crawl.ViewModels;
using Crawl.Services;
using System.Diagnostics;

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
            CreateTables();
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
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock elf Character", Description = "This is an Character description.", Level = 1, ImageURI = "https://66.media.tumblr.com/caee322a79f0cff2365a03b12ab8b8a6/tumblr_p4s334rGiB1x16ui8o1_400.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock dwarf Character", Description = "This is an Character description.", Level = 1, ImageURI = "http://www.darkquest2.com/images/Dwarf.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock magician Character", Description = "This is an Character description.", Level = 2, ImageURI = "https://i.imgur.com/ua6urzL.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock knight Character", Description = "This is an Character description.", Level = 2, ImageURI = "https://thumbs.gfycat.com/CalmSeriousBuckeyebutterfly-size_restricted.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock ninja Character", Description = "This is an Character description.", Level = 3, ImageURI = "https://orig00.deviantart.net/bc53/f/2017/234/5/3/ryu_hayabusa_standing_stance__2d__by_larsmasters-dbkq3vo.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQLMock fox Character", Description = "This is an Character description.", Level = 3, ImageURI = "https://i.pinimg.com/originals/33/ec/d2/33ecd27262385a3e03e4c2e573119364.gif" });

            // Monsters
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock dragon Monster", Description = "This is an Monster description.", ImageURI = "http://bestanimations.com/Fantasy/Dragons/dragon-animated-gif-60.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock ork Monster", Description = "This is an Monster description.", ImageURI = "http://www.darkquest2.com/images/orcWarrior.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock ogre Monster", Description = "This is an Monster description.", ImageURI = "https://www.arelitecore.com/images/ogre.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock 100 handed giant Monster", Description = "This is an Monster description.", ImageURI = "http://img3.wikia.nocookie.net/__cb20091108190157/mkwikia/images/6/66/GordanceYEA!.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock zombie Monster", Description = "This is an Monster description.", ImageURI = "https://cdna.artstation.com/p/assets/images/images/005/518/386/original/craig-mullins-zombie-boss-03-idle.gif?1491619273" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQLMock hellraiser Monster", Description = "This is an Monster description.", ImageURI = "http://www.ece.ubc.ca/~fengx/pics/Heroes4/death/devil.gif" });

            //scores
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  First Score", ScoreTotal = 111 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Second Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Third Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Fourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Fifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "SQLMock  Sixth Score", ScoreTotal = 666 });

        }

        #region Item
        // Item

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
            Debug.WriteLine("data base updated" + result); //for debugging
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
            var result = tempResult;
            return result;
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<Item>().ToListAsync();
            var result = new List<Item>();
            foreach (var item in tempResult)
            {
                result.Add(item);
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
            Debug.WriteLine("DB updated: " + result);

            if (result == 1)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            Debug.WriteLine("DB updated: " + result);
            if (result == 1)
            {
                return true;
            }

            return false;
        }


        public async Task<Score> GetAsync_Score(string id)
        {
            var tempResult = await App.Database.GetAsync<Score>(id);



            var result = tempResult;



            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<Score>().ToListAsync();



            var result = new List<Score>();

            foreach (var Score in tempResult)

            {

                result.Add(Score);

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