using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models

{

    public enum MenuItemType

    {

        //Browse, switched about and browse order

        About,

        Browse

        

    }

    public class HomeMenuItem

    {

        public MenuItemType Id { get; set; }



        public string Title { get; set; }

    }

}