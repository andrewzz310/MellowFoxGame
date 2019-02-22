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
            //algorithm described in class here
            return true;

        }
    }
}
