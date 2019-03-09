using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Crawl.Models;
using Crawl.ViewModels;
using System.Diagnostics;

namespace Crawl.GameEngine
{
    public class RoundEngine : TurnEngine
    {
        // Hold the list of players (monster, and character by guid), and order by speed
        public List<PlayerInfo> PlayerList;

        // Player currently engaged
        public PlayerInfo PlayerCurrent;
        public RoundEnum RoundStateEnum = RoundEnum.Unknown;

        public RoundEngine()
        {
            ClearLists();
        }

        // don't clear characters here because eventually they all get defeated by monsters
        private void ClearLists()
        {
            ItemPool = new List<Item>();
            MonsterList = new List<Monster>();
        }

        // Start the round, need to get the ItemPool, and Characters
        public void StartRound()
        {
            // Start on 0, the turns will increment...
            BattleScore.RoundCount = 0;

            // Start the first round...
            NewRound();

            Debug.WriteLine("Starting the Round # :" + BattleScore.RoundCount);
        }

        // new stuff with the battleviewmodel characters in here
        public void StartRound(BattleViewModel _instanceC)
        {
            BattleScore.RoundCount = 0;

            //new stuff
            int number = 0;
            var myData = _instanceC.SelectedCharacters[number];
            

            Debug.WriteLine("debug here to see if character is passed " + myData.Name);

            NewRound();
            Debug.WriteLine("Starting the Round # :" + BattleScore.RoundCount);

        }

        // Call to make a new set of monsters and initialize for roundnextturn()
        public void NewRound()
        {
            // End the existing round
            EndRound();

            // Populate New Monsters...
            AddMonstersToRound();

            // Make the PlayerList
            MakePlayerList();

            // Update Score for the RoundCount
            BattleScore.RoundCount++;
        }

        // Add Monsters
        // Scale them to meet Character Strength...
        private void AddMonstersToRound()
        {

            // Check to see if the monster list is full, if so, no need to add more...
            if (MonsterList.Count() >= 6)
            {
                return;
            }
            // TODO, determine the character strength
            
            // init monster scaling
            var ScaleLevelMax = 0;
            var ScaleLevelMin = 0;


            // Scale monsters based on round count.. higher round higher and stronger monsters
            if (BattleScore.RoundCount <= 10)
            {
                ScaleLevelMax = 1;
                ScaleLevelMin = 1;
            }

            if (BattleScore.RoundCount > 10 && BattleScore.RoundCount <= 30 )
            {
                ScaleLevelMax = 2;
                ScaleLevelMin = 1;
            }

            if (BattleScore.RoundCount > 30 && BattleScore.RoundCount <= 50)
            {
                ScaleLevelMax = 3;
                ScaleLevelMin = 2;
            }
            if (BattleScore.RoundCount > 50 && BattleScore.RoundCount <= 100)
            {
                ScaleLevelMax = 4;
                ScaleLevelMin = 2;
            }
            if (BattleScore.RoundCount > 100)
            {
                ScaleLevelMax = 10;
                ScaleLevelMin = 5;
            }


            // Make Sure Monster List exists and is loaded... (item is monster in this case not the actual items)
            var myMonsterViewModel = MonstersViewModel.Instance;
            myMonsterViewModel.ForceDataRefresh();

            if (myMonsterViewModel.Dataset.Count() > 0)
            {
                // Get 6 monsters based on the scaling that was set earlier and add items to monster
                do
                {
                    var rnd = HelperEngine.RollDice(1, myMonsterViewModel.Dataset.Count);
                    {
                        var mons = new Monster(myMonsterViewModel.Dataset[rnd - 1]);

                        // Help identify which monster it is...
                        mons.Name += " " + (1 + MonsterList.Count()).ToString();

                        var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
                        mons.ScaleLevel(rndScale);
                        MonsterList.Add(mons);
                    }

                } while (MonsterList.Count() < 6);
            }
            else
            {
                // No monsters in DB, so add 6 new ones...
                for (var i = 0; i < 6; i++)
                {
                    var item = new Monster();
                    // Help identify which monster it is...
                    item.Name += " " + MonsterList.Count() + 1;

                    MonsterList.Add(item);
                }
            }
        }

        // At the end of the round
        // Clear the Item List
        // Clear the Monster List
        // pick up items from pool
        public void EndRound()
        {
            // Have each character pickup items...
            foreach (var character in CharacterList)
            {
                // pick up items from the pool for each character (items go there if monster or character dies for item they were holding) 
                PickupItemsFromPool(character);
            }

            ClearLists();
        }

        // Get Round Turn Order

        // Rember Who's Turn


        // RoundNextTurn which uses PlayerCurrent as tracking which player
        public RoundEnum RoundNextTurn()
        {
            // No characters, game is over because game ends when characters die
            if (CharacterList.Count < 1)
            {
                // Game Over and exit do while loop from auto battle engine
                return RoundEnum.GameOver;
            }

            // Check if round is over when monsters are killed
            if (MonsterList.Count < 1)
            {
                // If over, New Round which loops back to main auto battle engine
                return RoundEnum.NewRound;
            }

           
            // Player current is the next players turn which is decided by recalculating the order 
            // through making a list based on attributes starting with speed, level, etc
            // Then parse through the list to get the next players turn
            PlayerCurrent = GetNextPlayerTurn();

            // Decide Who to Attack      
            if (PlayerCurrent.PlayerType == PlayerTypeEnum.Character)
            {
                // Get the player
                var myPlayer = CharacterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn by going to the turn engine and either having monster or character attack
                TakeTurn(myPlayer);
            }
            // Add Monster turn here...
            else if (PlayerCurrent.PlayerType == PlayerTypeEnum.Monster)
            {
                // Get the player
                var myPlayer = MonsterList.Where(a => a.Guid == PlayerCurrent.Guid).FirstOrDefault();

                // Do the turn....
                TakeTurn(myPlayer);
            }

            return RoundEnum.NextTurn;
        }

        // get the next monster/characters turn b
        public PlayerInfo GetNextPlayerTurn()
        {
            // Recalculate Order based on speed first
            OrderPlayerListByTurnOrder();

            var PlayerCurrent = GetNextPlayerInList();
            // Lookup CurrentPlayer in the list
            // Find the player next to Current Player in order

            return PlayerCurrent;
        }

        // initialize and then order the player list
        public void OrderPlayerListByTurnOrder()
        {
            var myReturn = new List<PlayerInfo>();

            // Order is based by... 
            // Order by Speed (Desending)
            // Then by Highest level (Descending)
            // Then by Highest Experience Points (Descending)
            // Then by Character before Monster (enum assending)
            // Then by Alphabetic on Name (Assending)
            // Then by First in list order (Assending

            // initialize list
            MakePlayerList();

            // organize list based on speed, level,etc,etc
            PlayerList = PlayerList.OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }
    
        // initializing list
        private void MakePlayerList()
        {
            PlayerList = new List<PlayerInfo>();
            PlayerInfo tempPlayer;

            var ListOrder = 0;

            foreach (var data in CharacterList)
            {
                if (data.Alive)
                {
                    tempPlayer = new PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }

            foreach (var data in MonsterList)
            {
                if (data.Alive)
                {

                    tempPlayer = new PlayerInfo(data);

                    // Remember the order
                    tempPlayer.ListOrder = ListOrder;

                    PlayerList.Add(tempPlayer);

                    ListOrder++;
                }
            }
        }

        // Traverse through the already organized player list based on speed,etc,etc
        public PlayerInfo GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // No current player, so set the last one, so it rolls over to the first...
            if (PlayerCurrent == null)
            {
                PlayerCurrent = PlayerList.LastOrDefault();
            }

            // Else go and pick the next player in the list...
            for (var i = 0; i < PlayerList.Count(); i++)
            {
                if (PlayerList[i].Guid == PlayerCurrent.Guid)
                {
                    if (i < PlayerList.Count() - 1) // 0 based...
                    {
                        return PlayerList[i + 1];
                    }
                    else
                    {
                        // Return the first in the list...
                        return PlayerList.FirstOrDefault();
                    }
                }
            }

            return null;
        }


        // pick up items from pool
        public void PickupItemsFromPool(Character character)
        {
            // Have the character, walk the items in the pool, and decide if any are better than current one.

            // No items in the pool...
            if (ItemPool.Count < 1)
            {
                return;
            }

            GetItemFromPoolIfBetter(character, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(character, ItemLocationEnum.Feet);
        }

        // Pick up items if better than current item
        public void GetItemFromPoolIfBetter(Character character, ItemLocationEnum setLocation)
        {
            var myList = ItemPool.Where(a => a.Location == setLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!myList.Any())
            {
                return;
            }

            var currentItem = character.GetItemByLocation(setLocation);
            if (currentItem == null)
            {
                // If no item in the slot then put on the first in the list
                character.AddItem(setLocation, myList.FirstOrDefault().Id);
                return;
            }

            foreach (var item in myList)
            {
                if (item.Value > currentItem.Value)
                {
                    // Put on the new item, which drops the one back to the pool
                    var droppedItem = character.AddItem(setLocation, item.Id);

                    // Remove the item just put on from the pool
                    ItemPool.Remove(item);

                    if (droppedItem != null)
                    {
                        // Add the dropped item to the pool
                        ItemPool.Add(droppedItem);
                    }
                }
            }
        }

    }
}
