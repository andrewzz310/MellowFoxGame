using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawl.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class CharacterUnitTests
    {
        [Test]
        public void Character_ScaleLevel_1_Should_Pass()
        {
            // Arrange
            var Test = new Character();
            int Level = 1;
            bool Expected = true;

            // Act
            var Actual = Test.ScaleLevel(Level);

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_0_Should_Fail()
        {
            // Arrange
            var Test = new Character();
            int Level = 0;
            bool Expected = false;

            // Act
            var Actual = Test.ScaleLevel(Level);

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_Neg1_Should_Fail()
        {
            // Arrange
            var Test = new Character();
            int Level = 0;
            bool Expected = false;

            // Act
            var Actual = Test.ScaleLevel(Level);

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }



        [Test]
        public void Character_ScaleLevel_MaxLevel_Plus_Should_Fail()
        {
            // Arrange
            var Test = new Character();
            int Level = LevelTable.MaxLevel + 1;
            bool Expected = false;

            // Act
            var Actual = Test.ScaleLevel(Level);

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Character_ScaleLevel_Lower_Level_Should_Fail()
        {
            // Arrange
            var Test = new Character();
            int Level = 2;
            bool Expected = false;

            // Set Character Level, by Scaling Up...
            Test.ScaleLevel(2);

            // Act
            var Actual = Test.ScaleLevel(Level - 1);

            // Reset

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

    }
}