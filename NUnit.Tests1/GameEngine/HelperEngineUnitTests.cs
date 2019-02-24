using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.GameEngine
{
    [TestFixture]
    public class HelperEngineUnitTests
    {
        [Test]
        public void RollDice_Roll_1_Dice_10_Should_Pass()
        {
            // Arrange
            var Roll = 1;
            var Dice = 10;

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.NotZero(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_2_Dice_10_Should_Pass()
        {
            // Arrange
            var Roll = 2;
            var Dice = 10;

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.NotZero(Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_0_Dice_10_Should_Fail()
        {
            // Arrange
            var Roll = 0;
            var Dice = 10;
            var ExpectedValue = 0; //Defensive check should return 0 meaning fail

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.AreEqual(ExpectedValue, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_Neg1_Dice_10_Should_Fail()
        {
            // Arrange
            var Roll = -1;
            var Dice = 10;
            var ExpectedValue = 0; //Defensive check should return 0 meaning fail

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.AreEqual(ExpectedValue, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_1_Dice_Neg1_Should_Fail()
        {
            // Arrange
            var Roll = 1;
            var Dice = -1;
            var ExpectedValue = 0; //Defensive check should return 0 meaning fail

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.AreEqual(ExpectedValue, Actual, TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void RollDice_Roll_1_Dice_Zero_Should_Fail()
        {
            // Arrange
            var Roll = 1;
            var Dice = 0;
            var ExpectedValue = 0; //Defensive check should return 0 meaning fail

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Assert
            Assert.AreEqual(ExpectedValue, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_1_Dice_10_Fixed_5_Should_Return_5()
        {
            // Arrange
            var Roll = 1;
            var Dice = 10;
            var Expected = 5;

            // Force RollDice to return a 5
            Crawl.Models.GameGlobals.SetForcedRandomNumbers(5);

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Reset
            Crawl.Models.GameGlobals.DisableRandomValues();

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RollDice_Roll_3_Dice_10_Fixed_5_Should_Return_15()
        {
            // Arrange
            var Roll = 3;
            var Dice = 10;
            var Expected = 15;

            // Force RollDice to return a 5
            Crawl.Models.GameGlobals.SetForcedRandomNumbers(5);

            // Act
            var Actual = Crawl.GameEngine.HelperEngine.RollDice(Roll, Dice);

            // Reset
            Crawl.Models.GameGlobals.DisableRandomValues();

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }
    }
}
}
