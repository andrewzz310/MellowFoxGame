using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.GameEngine
{
    // Battle is the top structure


    public class BattleEngine : RoundEngine
    {
        // The status of the actual battle, running or not (over)
        private bool isBattleRunning = false;
        


        // Constructor calls Init
        public BattleEngine()
        {
            BattleEngineInit();
        }

        

        // Sets the new state for the variables for Battle
        private void BattleEngineInit()
        {
            BattleScore = new Score();
            CharacterList = new List<Character>();
            ItemPool = new List<Item>();
        }

        // Determine if Auto Battle is On or Off
        public bool GetAutoBattleState()
        {
            return BattleScore.AutoBattle;
        }

        // Return if the Battle is Still running
        public bool BattleRunningState()
        {
            return isBattleRunning;
        }
        /// <summary>
        /// Returns the Score from the current Battle Instance
        /// </summary>
        /// <returns>the score value</returns>
        public int GetScoreValue()
        {
            return BattleScore.ScoreTotal;
        }
        //returns the current score object
        public Score GetScoreObject()
        {
            return BattleScore;
        }

        // Battle is over
        // Update Battle State, Log Score to Database
        public void EndBattle()
        {
            // Set Score
            BattleScore.ScoreTotal = BattleScore.ExperienceGainedTotal;

            // Set off state
            isBattleRunning = false;

            // Save the Score to the DataStore
            ScoresViewModel.Instance.AddAsync(BattleScore).GetAwaiter().GetResult();
        }

        // Initializes the Battle to begin
        public bool StartBattle(bool isAutoBattle)
        {
            // New Battle
            // Load the Characters
            BattleScore.AutoBattle = isAutoBattle;
            isBattleRunning = true;

            // Characters not Initialized, so false start...
            if (CharacterList.Count < 1)
            {
                return false;
            }
            return true;
        }

        // Add Characters
        // Scale them to meet Character Strength...
        public bool AddCharactersToBattle()
        {
            // Check to see if the Character list is full, if so return.
            if (CharacterList.Count >= 6)
            {
                return true;
            }

            // TODO, determine the character strength
            // add Characters up to that strength...
            var ScaleLevelMax = 2;
            var ScaleLevelMin = 1;

            if (CharactersViewModel.Instance.Dataset.Count < 1)
            {
                return false;
            }

            // Get exactly 6 charactersrounds
            do
            {
                var myData = GetRandomCharacter(ScaleLevelMin, ScaleLevelMax);
                CharacterList.Add(myData);
            } while (CharacterList.Count < 6);

            return true;
        }

        // add characters based on the battleviewmodel for our battle
        public bool AddCharactersToBattle(BattleViewModel _instanceC)
        {
            // Check to see if the Character list is full, if so return.
            if (CharacterList.Count >= 6)
            {
                return true;
            }

            // TODO, determine the character strength
            // add Characters up to that strength...
            var ScaleLevelMax = 2;
            var ScaleLevelMin = 1;

            if (BattleViewModel.Instance.SelectedCharacters.Count < 1)
            {
                return false;
            }
            int number = 0;
            // Get exactly 6 charactersrounds
            do
            {
                var myData = GetCharacterProperties(_instanceC, number, ScaleLevelMin, ScaleLevelMax);
                CharacterList.Add(myData);
                number++;
            } while (CharacterList.Count < 6);

            return true;
        }
        // get character properties for our list.
        public Character GetCharacterProperties(BattleViewModel _instanceC, int number, int ScaleLevelMin, int ScaleLevelMax)
        {
            //var myCharacterViewModel = CharactersViewModel.Instance;

            //var rnd = HelperEngine.RollDice(1, myCharacterViewModel.Dataset.Count);

            //var myData = new Character(_instanceC.DatasetChars[number]);

            //this helps us keep the characterlist as the battleviewmodel characters
            var myData = _instanceC.SelectedCharacters[number];

            // Help identify which Character it is...
            myData.Name += " " + (1 + CharacterList.Count).ToString();

            // scale based on roll
            var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
            myData.ScaleLevel(rndScale);

            // Add Items...
            myData.Head = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Head, AttributeEnum.Unknown);
            myData.Necklass = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Necklass, AttributeEnum.Unknown);
            myData.PrimaryHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.PrimaryHand, AttributeEnum.Unknown);
            myData.OffHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.OffHand, AttributeEnum.Unknown);
            myData.RightFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.RightFinger, AttributeEnum.Unknown);
            myData.LeftFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.LeftFinger, AttributeEnum.Unknown);
            myData.Feet = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);

            return myData;
        }


        // get random characters and attach neccesary items and level
        public Character GetRandomCharacter(int ScaleLevelMin, int ScaleLevelMax)
        {
            var myCharacterViewModel = CharactersViewModel.Instance;

            var rnd = HelperEngine.RollDice(1, myCharacterViewModel.Dataset.Count);

            var myData = new Character(myCharacterViewModel.Dataset[rnd - 1]);
          

            // Help identify which Character it is...
            myData.Name += " " + (1 + CharacterList.Count).ToString();

            // scale based on roll
            var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
            myData.ScaleLevel(rndScale);

            // Add Items...
            myData.Head = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Head, AttributeEnum.Unknown);
            myData.Necklass = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Necklass, AttributeEnum.Unknown);
            myData.PrimaryHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.PrimaryHand, AttributeEnum.Unknown);
            myData.OffHand = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.OffHand, AttributeEnum.Unknown);
            myData.RightFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.RightFinger, AttributeEnum.Unknown);
            myData.LeftFinger = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.LeftFinger, AttributeEnum.Unknown);
            myData.Feet = ItemsViewModel.Instance.ChooseRandomItemString(ItemLocationEnum.Feet, AttributeEnum.Unknown);

            return myData;
        }


        /// <summary>
        /// Retruns a formated String of the Results of the Battle
        /// </summary>
        /// <returns></returns>
        public string GetResultsOutput()
        {
            string myResult =
                " ##################################### " + Environment.NewLine + Environment.NewLine +
            "MellowFoxBattle Ended! Score total is: " + BattleScore.ScoreTotal +
            "  Total Experience  :" + BattleScore.ExperienceGainedTotal +
            " Total Rounds :" + BattleScore.RoundCount +
            " Total Turns :" + BattleScore.TurnCount +
            " Total Monsters Defeated: " + BattleScore.MonsterSlainNumber;
            // " Total Monster Kills :" + BattleEngine.BattleScore.MonstersKilledList;

            Debug.WriteLine(myResult);

            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine(" Thank you for playing MellowFoxGame, see score detail page for info and play again!");
            Debug.WriteLine(" ##################################### ");
            Debug.WriteLine(Environment.NewLine);
            return myResult;
        }
        /*
        public bool AutoBattle()
        {
            return true;
        }
        */

    }
}
