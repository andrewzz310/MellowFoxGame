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
            // Database information
            Guid = newData.Guid;
            Id = newData.Id;
            Name = newData.Name;
            Description = newData.Description;
            Level = newData.Level;
            ExperienceTotal = newData.ExperienceTotal;
            ImageURI = newData.ImageURI;
            Item = newData.Item; //adding preferred item
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
        }

        // So when working with the database, pass Character
        public void Update(Monster newData)
        {
                return;
        }
    }
}