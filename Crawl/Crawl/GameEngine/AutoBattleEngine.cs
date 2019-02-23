using System;
using System.Collections.Generic;
using System.Text;
using Crawl.Models;
using System.Diagnostics;


namespace Crawl.GameEngine
{
    class AutoBattleEngine
    {
        // battle engine
        public BattleEngine BattleEngine = new BattleEngine();

        public bool RunAutoBattle()
        {
            // Picks 6 Characters
            if (BattleEngine.AddCharactersToBattle() == false)
            {
                // if not 6 characters added
                return false;
            }

            // Begin the battle here
            BattleEngine.StartBattle(true);

            //init console message
            Debug.WriteLine("The Mellow Fox Game Battle has Started" + " Characters :" + BattleEngine.CharacterList.Count);

            // Initialize the Rounds
            RoundEnum RoundResult;
            BattleEngine.StartRound();

            //do while looped discussed in class about fighting until the game is over
            do
            {
                //Each Turn
                RoundResult = BattleEngine.RoundNextTurn();

                // If the round is over start a new one...
                if (RoundResult == RoundEnum.NewRound)
                {
                    BattleEngine.NewRound();
                    Debug.WriteLine("New Round :" + BattleEngine.BattleScore.RoundCount);
                }

            } while (RoundResult != RoundEnum.GameOver);

            //algorithm described in class here
            return true;

        }
    }
}
