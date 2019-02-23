using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Crawl.Models;
using Crawl.ViewModels;

namespace Crawl.GameEngine
{
    // Battle is the top structure


    class BattleEngine : RoundEngine
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
            return true;
        }

        // Add Characters
        // Scale them to meet Character Strength...
        public bool AddCharactersToBattle()
        {
                return true;
        }

        public Character GetRandomCharacter(int ScaleLevelMin, int ScaleLevelMax)
        {
            var myData = new Character();
            return myData;
        }

        public bool AutoBattle()
        {
            return true;
        }

    }
}
