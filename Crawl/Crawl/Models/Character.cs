﻿using Crawl.ViewModels;
using System.Collections.Generic;
using Crawl.Controllers;
using Crawl.GameEngine;

namespace Crawl.Models
{
    // The Character is the higher level concept.  This is the Character with all attirbutes defined.
    public class Character : BaseCharacter
    {
        // Add in the actual attribute class
        public AttributeBase Attribute { get; set; }

        public Character()
        {
            Attribute = new AttributeBase();
            Alive = true;
        }
        // Create a default Character for the instantiation
        //private void CreateDefaultCharacter()
        //{
        //    Name = "Unknown";
        //    Description = "Unknown";
        //    ImageURI = ItemsController.DefaultImageURICharacter;
     
        //    // adding properties
        //    Level = 0;
        //    Alive = true;

        //    ExperienceTotal = 0;
        //}

        // Create a new character, based on a passed in BaseCharacter
        // Used for converting from database format to character
        public Character(BaseCharacter newData)
        {
            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ImageURI = newData.ImageURI;
            
            Alive = newData.Alive;
            ExperienceTotal = newData.ExperienceTotal;

            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Populate the Attributes
            AttributeString = newData.AttributeString;

            Attribute = new AttributeBase(newData.AttributeString);

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        // Create a new character, based on existing Character
        public Character(Character newData)
        {
            Update(newData);
        }


        //Constructor that takes params. needed to create a new item with set values
        //public Character(string name, string description, string imageuri, int level, int experiencetotal, int attack, int defense, int speed, PreferredItemEnum item)
        //{
        //    CreateDefaultCharacter();
        //    Name = name;
        //    Description = description;
        //    ImageURI = imageuri;
           
        //    ExperienceTotal = experiencetotal;
        //    Level = level;

        //}

        // Upgrades to a set level
        public bool ScaleLevel(int level)
        {
            // Level of < 1 does not need changing
            if (level < 1)
            {
                return false;
            }

            // Same level does not need changing
            if (level == this.Level)
            {
                return false;
            }

            // Don't go down in level...
            if (level < this.Level)
            {
                return false;
            }

            // Level > Max Level
            if (level > LevelTable.MaxLevel)
            {
                return false;
            }

            // Calculate Experience Remaining based on Lookup...
            Level = level;

            Attribute.MaxHealth = HelperEngine.RollDice(Level, HealthDice);

            return true;
        }

        // Update the character information
        // Updates the attribute string
        public void Update(Character newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id

            // Base information
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;

            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            // Populate the Attributes
            Attribute = newData.Attribute;

            // set the attribute string, for the Attribute
            AttributeString = AttributeBase.GetAttributeString(Attribute);

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = string.Empty;
            myReturn += Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            myReturn += " , Total Experience : " + ExperienceTotal;
            myReturn += " , " + Attribute.FormatOutput();
            myReturn += " , Items : " + ItemSlotsFormatOutput();
            myReturn += " Damage : " + GetDamageDice();

            return myReturn;
        }

        #region Basics
        // Level Up
        public bool LevelUp()
        {
            // Walk the Level Table descending order
            // Stop when experience is >= experience in the table
            for (var i = LevelTable.Instance.LevelDetailsList.Count - 1; i > 0; i--)
            {
                // Check the Level
                // If the Level is > Experience for the Index, increment the Level.
                if (LevelTable.Instance.LevelDetailsList[i].Experience <= ExperienceTotal)
                {
                    var NewLevel = LevelTable.Instance.LevelDetailsList[i].Level;

                    // When leveling up, the current health is adjusted up by an offset of the MaxHealth, rather than full restore
                    var OldCurrentHealth = Attribute.CurrentHealth;
                    var OldMaxHealth = Attribute.MaxHealth;

                    // Set new Health
                    // New health, is d10 of the new level.  So leveling up 1 level is 1 d10, leveling up 2 levels is 2 d10.
                    var NewHealthAddition = HelperEngine.RollDice(NewLevel - Level, 10);

                    // Increment the Max health
                    Attribute.MaxHealth += NewHealthAddition;

                    // Calculate new current health
                    // old max was 10, current health 8, new max is 15 so (15-(10-8)) = current health
                    Attribute.CurrentHealth = (Attribute.MaxHealth - (OldMaxHealth - OldCurrentHealth));

                    // Refresh the Attriburte String
                    AttributeString = AttributeBase.GetAttributeString(this.Attribute);

                    // Set the new level
                    Level = NewLevel;

                    // Done, exit
                    return true;
                }
            }

            return false;
        }

        // Level up to a number, say Level 3
        public int LevelUpToValue(int Value)
        {
            // Adjust the experience to the min for that level.
            // That will trigger level up to happen

            if (Value < 0)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value <= Level)
            {
                // Skip, and return old level
                return Level;
            }

            if (Value > LevelTable.MaxLevel)
            {
                Value = LevelTable.MaxLevel;
            }

            AddExperience(LevelTable.Instance.LevelDetailsList[Value].Experience + 1);

            return Level;
        }

        // Add experience
        public bool AddExperience(int newExperience)
        {
            // Don't allow going lower in experience
            if (newExperience < 0)
            {
                return false;
            }

            // Increment the Experience
            ExperienceTotal += newExperience;

            // Can't level UP if at max.
            if (Level == LevelTable.MaxLevel)
            {
                return false;
            }

            // Then check for Level UP
            // If experience is higher than the experience at the next level, level up is OK.
            if (ExperienceTotal >= LevelTable.Instance.LevelDetailsList[Level + 1].Experience)
            {
                return LevelUp();
            }
            return false;
        }


        #endregion Basics

        #region GetAttributes
        // Get Attributes

        // Get Attack
        public int GetAttack()
        {
            // Base Attack
            var myReturn = Attribute.Attack;

            // Implement

            // Attack Bonus from Level

            // Get Attack bonus from Items

            return myReturn;
        }

        // Get Speed
        public int GetSpeed()
        {
            // Base value
            var myReturn = Attribute.Speed;

            // Get Bonus from Level

            myReturn += LevelTable.Instance.LevelDetailsList[Level].Speed;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Speed);

            return myReturn;
        }

        // Get Defense
        public int GetDefense()
        {
            // Base value
            var myReturn = Attribute.Defense;

            // Get Bonus from Level
            myReturn += LevelTable.Instance.LevelDetailsList[Level].Defense;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.Defense);

            return myReturn;
        }

        // Get Max Health
        public int GetHealthMax()
        {
            // Base value
            var myReturn = Attribute.MaxHealth;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.MaxHealth);

            return myReturn;
        }

        // Get Current Health
        public int GetHealthCurrent()
        {
            // Base value
            var myReturn = Attribute.CurrentHealth;

            // Get bonus from Items
            myReturn += GetItemBonus(AttributeEnum.CurrentHealth);

            return myReturn;

        }

        // Returns the Dice for the item
        // Sword 10, is Sword Dice 10
        public int GetDamageDice()
        {
            var myReturn = 0;

            var myItem = ItemsViewModel.Instance.GetItem(PrimaryHand);
            if (myItem != null)
            {
                // Damage is base damage plus dice of the weapon.  So sword of Damage 10 is d10
                myReturn += myItem.Damage;
            }

            return myReturn;
        }

        // Get the Level based damage
        // Then add the damage for the primary hand item as a Dice Roll
        public int GetDamageRollValue()
        {
            var myReturn = GetLevelBasedDamage();

            var myItem = ItemsViewModel.Instance.GetItem(PrimaryHand);
            if (myItem != null)
            {
                // Damage is base damage plus dice of the weapon.  So sword of Damage 10 is d10
                myReturn += HelperEngine.RollDice(1, myItem.Damage);
            }


            return myReturn;
        }

        #endregion GetAttributes

        #region Items
        // Drop All Items
        // Return a list of items for the pool of items
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Implement

            // Drop all Items
            
            return myReturn;
        }

        // Remove Item from a set location
        // Does this by adding a new item of Null to the location
        // This will return the previous item, and put null in its place
        // Returns the item that was at the location
        // Nulls out the location
        public Item RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);
        }

        // Get the Item at a known string location (head, foot etc.)
        public Item GetItemByLocation(ItemLocationEnum itemLocation)
        {
            // Implement

            return null;
        }

        // Add Item
        // Looks up the Item
        // Puts the Item ID as a string in the location slot
        // If item is null, then puts null in the slot
        // Returns the item that was in the location
        public Item AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            Item myReturn = new Item();

            // Implement

            return myReturn;
        }

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            Item myItem;
            // Implement

            return myReturn;
        }

        #endregion Items

        // Take Damage
        // If the damage recived, is > health, then death occurs
        // Return the number of experience received for this attack 
        // monsters give experience to characters.  Characters don't accept expereince from monsters
        public void TakeDamage(int damage)
        {
            if (damage < 1)
            {
                return;
            }

            Attribute.CurrentHealth -= damage;
            if (GetHealthCurrent() <= 0)
            {
                // Death...
                CauseDeath();
            }
        }
    }
}