using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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
                List<string> myList = new List<string>();
                string myName = "empty";

                myList.Add(myName);
               
                return myList;
            }
        }
    }
}
