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
        private async void InitilizeSeedData()
        {

            // Load Items
            //_itemDataset.Add(new Item("Shuriken", "This is a Shuriken  Item", "shuriken.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            //_itemDataset.Add(new Item("Armor", "This is a Armor Item", "armors.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            //_itemDataset.Add(new Item("Ring of Power", "This is a Ring of Power Item", "ringofpower.png", 1, 5, 0, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            //_itemDataset.Add(new Item("Two-Sided Hammer", "This is a Two-Sided Hammer Item", "hammer1.png", 2, 5, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            //_itemDataset.Add(new Item("Bow and Arrow", "This is a Bow and Arrow Item", "bowandarrows.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            //_itemDataset.Add(new Item("Turbo", "This is a Turbo Item", "turbo.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.Speed));
            //_itemDataset.Add(new Item("Staff Sword", "This is a Staff Sword Item", "sword.png", 3, 7, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            //_itemDataset.Add(new Item("Potion", "This is a Potion Item", "potion.png", 10, 6, 7, ItemLocationEnum.OffHand, AttributeEnum.CurrentHealth));


            //items
            await AddAsync_Item(new Item("Knight Sword", "Sword of Power. Gives the knight super strength", "https://66.media.tumblr.com/0a842748fd2d4007774d1046fb409665/tumblr_p3cp5dOgyY1si01xjo1_400.gif", 10, 3, 3, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Ninja's Shuriken", "Shuriken made of steel. Strikes far and true", "https://cdnb.artstation.com/p/assets/images/images/016/136/149/original/paulo-silva-shuriken.gif?1551046439", 3, 7, 7, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Shield of Justice", "Protects against the strongests strikes", "https://data.whicdn.com/images/278800576/original.gif", 0, 5, 5, ItemLocationEnum.OffHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("Ring of Power", "Gives Wearer incredible strngth", "https://thumbs.gfycat.com/AptUnkemptBernesemountaindog-small.gif", 1, 5, 5, ItemLocationEnum.Finger, AttributeEnum.Defense));
            await AddAsync_Item(new Item("Two-Sided Hammer", "Steel hammer, can dismemeber oponents", "https://vignette1.wikia.nocookie.net/fj-items-database/images/7/7a/Golden_Hammer.gif/revision/latest?cb=20151209190942", 2, 5, 5, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Bow and Arrow", "Bow and Arrow, accurately spears oponenets", "https://mir-s3-cdn-cf.behance.net/project_modules/disp/ee1e7118503601.562ca8bb847da.gif", 10, 6, 6, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Magician's Staff", "Blasts balls of fire and electricity", "https://media2.giphy.com/media/ZUEdlLMjM6M5q/source.gif", 10, 6, 6, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Potion", "Revitalizes health", "https://orig00.deviantart.net/deb3/f/2016/199/a/5/potion_by_saramfdraws-daaiys0.gif", 10, 6, 6, ItemLocationEnum.OffHand, AttributeEnum.MaxHealth));
            await AddAsync_Item(new Item("club", "Club with spikes", "https://sites.google.com/site/pathfinderogc/_/rsrc/1487036772539/images/club.png", 9, 7, 7, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("chains", "Chains can mame and strangle", "http://www.rdpnorthernalbania.org/upload/2017/11/23/animation-of-a-bike-chain-3d-animation-with-pov-ray-bicycles-chain-l-5467439e05d6f754.gif", 8, 9, 9, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("two sided axe", "Two Sided Axe can dismember oponent", "http://www.clker.com/cliparts/7/9/a/8/12161799982075078511StefanvonHalenbach_Battle_axe_medieval.svg.hi.png", 7, 9, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("razor sharp claw", "Razor Sharp Claw can cut through metal", "https://banner2.kisspng.com/20180402/iwq/kisspng-claw-metal-paper-steel-claw-5ac27418af6031.7615608715226931447184.jpg", 8, 5, 5, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("cape of flight", "Cape Of Flight to fly", "https://assets.activedemand.com/content_images/81867/images/original/hero-cape-trans.gif?1497905919", 9, 4, 4, ItemLocationEnum.Necklass, AttributeEnum.Speed));
            await AddAsync_Item(new Item("pharo's crown", "Pharo's Crown can protect and heal", "https://i.imgur.com/BeYNw1V.gif", 10, 7, 7, ItemLocationEnum.Head, AttributeEnum.CurrentHealth));
            await AddAsync_Item(new Item("Raa's Evil Eye", "Raa's Evil Eye blasts laser from the eyes", "https://fabrika-antey.ru/images/eyeball-clipart-eye-check-4.png", 10, 8, 8, ItemLocationEnum.Head, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Bracelet of Thunder", "Defends against strikes", "https://gamepedia.cursecdn.com/skyrim_gamepedia/d/d1/HideBracersofAlchemy.png", 7, 9, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Defense));
            await AddAsync_Item(new Item("The Tablet of Hamurabi", "The Tablet of Hamurabi, summons army of death", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/Stele_of_Vultures_detail_01-transparent.png/280px-Stele_of_Vultures_detail_01-transparent.png", 9, 10, 10, ItemLocationEnum.OffHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Capone's Gun", "Al Capone's Gun, shoots bullets", "http://pngimg.com/uploads/gangster/gangster_PNG3.png", 8, 9, 9, ItemLocationEnum.PrimaryHand, AttributeEnum.Attack));
            await AddAsync_Item(new Item("Superman's Glasses", "Morphs the character and gives him strong abilities", "https://i.pinimg.com/originals/7c/fc/b1/7cfcb132a526835b69dd238f8a8d7e4f.png", 10, 3, 3, ItemLocationEnum.Head, AttributeEnum.Speed));

            //characters
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock elf character", Description = "This is an Character description.", Level = 1, ImageURI = "https://66.media.tumblr.com/caee322a79f0cff2365a03b12ab8b8a6/tumblr_p4s334rGiB1x16ui8o1_400.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock dwarf Character", Description = "This is an Character description.", Level = 1, ImageURI = "http://www.darkquest2.com/images/Dwarf.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock magician Character", Description = "This is an Character description.", Level = 2, ImageURI = "https://i.imgur.com/ua6urzL.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock knight Character", Description = "This is an Character description.", Level = 2, ImageURI = "https://thumbs.gfycat.com/CalmSeriousBuckeyebutterfly-size_restricted.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock ninja Character", Description = "This is an Character description.", Level = 3, ImageURI = "https://orig00.deviantart.net/bc53/f/2017/234/5/3/ryu_hayabusa_standing_stance__2d__by_larsmasters-dbkq3vo.gif" });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Mock fox Character", Description = "This is an Character description.", Level = 3, ImageURI = "https://i.pinimg.com/originals/33/ec/d2/33ecd27262385a3e03e4c2e573119364.gif" });

            // Monsters
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock dragon Monster", Description = "This is an Monster description.", ImageURI = "http://bestanimations.com/Fantasy/Dragons/dragon-animated-gif-60.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock ork Monster", Description = "This is an Monster description.", ImageURI = "http://www.darkquest2.com/images/orcWarrior.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock ogre Monster", Description = "This is an Monster description.", ImageURI = "https://www.arelitecore.com/images/ogre.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock 100 handed giant Monster", Description = "This is an Monster description.", ImageURI = "http://img3.wikia.nocookie.net/__cb20091108190157/mkwikia/images/6/66/GordanceYEA!.gif" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock zombie Monster", Description = "This is an Monster description.", ImageURI = "https://cdna.artstation.com/p/assets/images/images/005/518/386/original/craig-mullins-zombie-boss-03-idle.gif?1491619273" });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Mock hellraiser Monster", Description = "This is an Monster description.", ImageURI = "http://www.ece.ubc.ca/~fengx/pics/Heroes4/death/devil.gif" });

            //scores
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  First Score", ScoreTotal = 111 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Second Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Third Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Fourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Fifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Mock  Sixth Score", ScoreTotal = 666 });


            // Characters initialized with level 2, xp 300, etc based on ppt deck
            //_characterDataset.Add(new Character("Elf", "Special power is bow and arrow item", "elf.png",  2, 300, 1,2,1, PreferredItemEnum.Armor));
            //_characterDataset.Add(new Character("Dwarf", "Special power is hammer item", "Dwarf.png",  2, 300,1,2,1, PreferredItemEnum.BowArrow));
            //_characterDataset.Add(new Character("Magician", "Special power is staff item", "magician.png",  2, 300,1,2,1, PreferredItemEnum.RingOfPower));
            //_characterDataset.Add(new Character("Knight", "Special power is sword item", "Knight.png",  2, 300,1,2,1, PreferredItemEnum.Shuriken));
            //_characterDataset.Add(new Character("Ninja", "Special power is damage without any items", "ninja.png",  2, 300,1,2,1, PreferredItemEnum.Staff));
            //_characterDataset.Add(new Character("Mellow Fox", "Special power is automatically skips level ", "fox.png",  2, 300,1,2,1, PreferredItemEnum.Sword));


            //// Monsters initialized 
            //_monsterDataset.Add(new Monster("Dragon", "This is a Dragon monster", "dragon1.png", PreferredItemEnum.Armor));
            //_monsterDataset.Add(new Monster("Ork", "This is a Ork monster", "ork.png",PreferredItemEnum.BowArrow));
            //_monsterDataset.Add(new Monster("Ogre", "This is a Ogre monster", "ogre.png", PreferredItemEnum.RingOfPower));
            //_monsterDataset.Add(new Monster("100 Handed Giant", "This is a 100 handed giant monster", "100giant.png", PreferredItemEnum.Shuriken));
            //_monsterDataset.Add(new Monster("Zombie", "This is a Zombie monster", "zombie.png", PreferredItemEnum.Staff));
            //_monsterDataset.Add(new Monster("Hellraiser", "This is a Hellraiser monster", "hellraiser.png", PreferredItemEnum.Sword));

            // Implement Scores
            //_scoreDataset.Add(new Score(1, 2, DateTime.Now, true, 3, 4, 5, 6, "Elf", "Dragon", "Bow and Arrow"));

        }

        private void CreateTables()
        {
            // Do nothing...
        }

        // Delete the Datbase Tables by dropping them
        public void DeleteTables()
        {
            // Implement
            _characterDataset.Clear();
            _itemDataset.Clear();
            _monsterDataset.Clear();
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

            //Scores
            ScoresViewModel.Instance.SetNeedsRefresh(true);

            //battleviewmodel
              BattleViewModel.Instance.SetNeedsRefresh(true);
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


        public async Task<bool> InsertUpdateAsync_Score(Score data)
        {

            // Check to see if the score exists
            var oldData = await GetAsync_Score(data.Id);
            if (oldData == null)
            {
                _scoreDataset.Add(data);
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
            _scoreDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }
        #endregion Score
    }
}