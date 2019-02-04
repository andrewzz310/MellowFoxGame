using Crawl.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawl.Models
{
    //List of Items for Picker on new charcater/update pages

 

    public static class ItemList
    {

        // Helper functions to get list of items
        public static List<string> GetListItem
        {
            get
            {
                List<string> myList = new List<string>
                {
                    "Staff",
                    "Sword",
                    "Two-Sided Hammer",
                    "Bow and Arrow",
                    "Ring of Power",
                    "Armor",
                    "Shuriken"
                };

                return myList;
            }
        }
    }
}
