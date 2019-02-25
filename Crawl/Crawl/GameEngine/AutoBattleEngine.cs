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

            //init console message and characters
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("The Mellow Fox Game Battle has Started" + " Total Characters :" + BattleEngine.CharacterList.Count);
            Debug.WriteLine("Characters are Below :");
            for (var i = 0; i < 6; i++)
            { 
                Debug.WriteLine(BattleEngine.CharacterList[i].FormatOutput());
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);


            // Initialize the Rounds and populates new monsters with addmonsterstoround()
            BattleEngine.StartRound();
            RoundEnum RoundResult;

            //Message with Monsters
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("Round has Started" + " Total Monsters # are :" + BattleEngine.MonsterList.Count);
            Debug.WriteLine("Monsters are below :");
            for (var i = 0; i < 6; i++)
            {
                Debug.WriteLine(BattleEngine.MonsterList[i].FormatOutput());
            }
            Debug.WriteLine("########################################");
            Debug.WriteLine(Environment.NewLine);


       

      
            //do while looped discussed in class about fighting until the game is over
            do
            {
                //Each Turn
                RoundResult = BattleEngine.RoundNextTurn(); 

                // If the round is over start a new one...
                if (RoundResult == RoundEnum.NewRound)
                {
                    BattleEngine.NewRound();
                    Debug.WriteLine("New Round#:" + BattleEngine.BattleScore.RoundCount);
                }

            } while (RoundResult != RoundEnum.GameOver);

            // battle ended and update the scoredetail view model
            BattleEngine.EndBattle();

            
            return true;

        }


        /// <summary>
        /// Returns the Score from the current Battle Instance
        /// </summary>
        /// <returns>the score value</returns>
        public int GetScoreValue()
        {
            return BattleEngine.BattleScore.ScoreTotal;
        }

        /// <summary>
        /// Returns the current Score Object
        /// </summary>
        /// <returns>Current Score Object</returns>
        public Score GetScoreObject()
        {
            return BattleEngine.BattleScore;
        }

        /// <summary>
        /// Returns the number of Rounds in the battle
        /// </summary>
        /// <returns>the count of rounds</returns>
        public int GetRoundsValue()
        {
            return BattleEngine.BattleScore.RoundCount;
        }

        /// <summary>
        /// Retruns a formated String of the Results of the Battle
        /// </summary>
        /// <returns></returns>
        public string GetResultsOutput()
        {
            string myResult =
                " ##################################### " + Environment.NewLine + Environment.NewLine +
            "MellowFoxBattle Ended! Score total is: " + BattleEngine.BattleScore.ScoreTotal +
            "  Total Experience  :" + BattleEngine.BattleScore.ExperienceGainedTotal +
            " Total Rounds :" + BattleEngine.BattleScore.RoundCount +
            " Total Turns :" + BattleEngine.BattleScore.TurnCount;
           // " Total Monster Kills :" + BattleEngine.BattleScore.MonstersKilledList;

            Debug.WriteLine(myResult);
            
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine(" Thank you for playing MellowFoxGame, see score detail page for info and play again!");
            Debug.WriteLine(" ##################################### ");
            return myResult;
        }


    }
}
