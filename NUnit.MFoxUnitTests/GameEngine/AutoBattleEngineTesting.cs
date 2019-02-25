using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Crawl.GameEngine;

namespace NUnit.MFoxUnitTests.GameEngine
{
    [TestFixture]
    public class AutoBattleEngineTesting
    {
        /// <summary>
        /// Validate that a new Auto Battle Instace is created.  
        /// Constructor should not be null
        /// </summary>
        [Test]
        public void AutoBattleEngine_InitiateBattle_Should_Pass()
        {

            // Arrange (No need to set a variable first)

            // Act (The actual return should be that the battle engine started)
            var Actual = new AutoBattleEngine();

            // Reset

            // Assert (Make sure that a game is initiated)
            Assert.AreNotEqual(null, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Get the score at game initiation
        /// Because the battle has not run, it will be the default value for score, which is zero
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetScoreValueAtStart_Should_Pass()
        {

            // Arrange (Initiate a new Auto Battle)
            var startBattle = new AutoBattleEngine();
            var Expected = 0;   // 0 is the Expected score, because game just started)

            // Act (Actual score of the battle)
            var Actual = startBattle.GetScoreValue();

            // Reset

            // Assert (Should return 0)
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        ///Get the Rounds Value at initiation
        /// Because the battle has not run, it will be the default value which is zero
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetRoundsValueAtStart_Should_Pass()
        {

            // Arrange
            var startBattle = new AutoBattleEngine();
            var Expected = 0;   // 0 because it is not defined yet

            // Act
            var Actual = startBattle.GetRoundsValue();

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        ///Get the Output result at initiation
        /// Because the battle has not run, it will be the default value which empty string
        /// </summary>
        [Test]
        public void AutoBattleEngine_GetResultsOutputAtStart_Should_Pass()
        {

            // Arrange
            var startBattle = new AutoBattleEngine();

            // Act

            var Actual = startBattle.GetResultsOutput();

            // Reset

            // Assert
            Assert.NotNull(Actual, TestContext.CurrentContext.Test.Name);
        }
    }
}
