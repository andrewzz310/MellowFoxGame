using App1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views

{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : MasterDetailPage

    {

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()

        {

            InitializeComponent();



            MasterBehavior = MasterBehavior.Popover;



            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);



            //this instantiates  as the main navigation page

            MenuPages.Add((int)MenuItemType.About, (NavigationPage)Detail);

        }



        public async Task NavigateFromMenu(int id)

        {

            if (!MenuPages.ContainsKey(id))

            {

                switch (id)

                {



                    case (int)MenuItemType.About:

                        MenuPages.Add(id, new NavigationPage(new AboutPage()));

                        break;

                    //switched order of about and browse

                    case (int)MenuItemType.Browse:

                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));

                        break;



                    //newly added for testing chracter page

                    case (int)MenuItemType.MellowFoxGame:

                        MenuPages.Add(id, new NavigationPage(new MellowFoxGame()));

                        break;



                    //newly added for testing score page

                    case (int)MenuItemType.ScorePage:

                        MenuPages.Add(id, new NavigationPage(new ScorePage()));

                        break;



                    //newly added for testing Character Page

                    case (int)MenuItemType.CharacterPage:

                        MenuPages.Add(id, new NavigationPage(new CharacterPage()));

                        break;

                }

            }



            var newPage = MenuPages[id];



            if (newPage != null && Detail != newPage)

            {

                Detail = newPage;



                if (Device.RuntimePlatform == Device.Android)

                    await Task.Delay(100);



                IsPresented = false;

            }

        }

    }

}