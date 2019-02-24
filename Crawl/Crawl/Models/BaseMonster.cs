﻿namespace Crawl.Models
{
    public class BaseMonster : BasePlayer<BaseMonster>
    {
        // Unique Item for Monster
        public string UniqueItem { get; set; }

        // Damage the Monster can do.
        public int Damage { get; set; }

        // Just base from here down. 
        // This is what will be saved to the Database

        public BaseMonster()
        {

        }

        // Creaste a base from a monster, this reuses the guid and id
        public BaseMonster(Monster newData)
        {
            // Database information
            Guid = newData.Guid;
            Id = newData.Id;

            Update(newData);

        }

        // So when working with the database, pass Monster
        public void Update(Monster newData)
        {
            if (newData == null)
            {
                return;
            }

            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Alive = newData.Alive;

            // Populate the Attributes
            AttributeString = newData.AttributeString;

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Calculate Experience Remaining based on Lookup...
            ExperienceTotal = LevelTable.Instance.LevelDetailsList[Level].Experience;

            Damage = newData.Damage;
            return;
        }
    }
}

/*
using System;
using SQLite;
using Crawl.Controllers;
using Crawl.ViewModels;

namespace Crawl.Models
{
    public class BaseMonster : BasePlayer<BaseMonster>
    {
        // <HV> need to remove this as it does not apply mellofox game specs
        public string UniqueItem { get; set; }

        // Damage the Monster can do.
        public int Damage { get; set; }

        // Just base from here down. 
        // This is what will be saved to the Database

        public BaseMonster()
        {

        }

        // Create a base from a monster, this reuses the guid and id
        public BaseMonster(Monster newData)
        {
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
            AttributeString = newData.AttributeString;

            // Set the strings for the items
            Head = newData.Head;
            Feet = newData.Feet;
            Necklass = newData.Necklass;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            Feet = newData.Feet;
            UniqueItem = newData.UniqueItem;

            // Calculate Experience Remaining based on Lookup...
            ExperienceTotal = LevelTable.Instance.LevelDetailsList[Level].Experience;

            Damage = newData.Damage;
        }

        // So when working with the database, pass Character
        public void Update(Monster newData)
        {
                return;
        }
    }
}
*/
