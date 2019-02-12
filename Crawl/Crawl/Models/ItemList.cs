using Crawl.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawl.Models
{
    //Enum for preferred items
    public enum PreferredItemEnum
    {
        // Not specified
        Unknown = 0,

        //Preferred Items and enum value 
        Staff = 10,
        Sword = 11,
        TwoSidedHammer = 12,
        BowArrow = 13,
        RingOfPower = 14,
        Armor = 15,
        Shuriken = 16
    }
    public static class ItemList
    {
        // Helper functions to get list of items
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(PreferredItemEnum)).ToList();
                var myReturn = myList; 
                return myReturn;
            }
        }
    }
}
