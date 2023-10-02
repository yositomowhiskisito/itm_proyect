using System;
using Xamarin.Forms;
using XAM_ProyectITM.Core;
using System.Runtime.CompilerServices;
using LIBUtilities.Core;
using Xamarin.CommunityToolkit.UI.Views;

namespace XAM_ProyectITM.Screens.Phone
{
    public class MainWindow : MasterDetailPage
    {
        private MenuPage menuPage;

        public MainWindow()
        {
            GlobalData.MasterDetailPage = this;
            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) =>
                NavigateTo(e.SelectedItem as Core.MenuItem);
            Master = menuPage;

            var login = (Page)new Login();
            Page displayPage = login;

            if(displayPage == login)
                GlobalData.MasterDetailPage.IsGestureEnabled = false;

            displayPage.Title = "ITM Proyect";
            Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
        }

        private void NavigateTo(Core.MenuItem menu)
        {
            if (menu == null)
                return;
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            displayPage.Title = "ITM Proyect";
            NavigationPage.SetHasNavigationBar(this, false);
            GlobalData.MasterDetailPage.IsGestureEnabled = true;
            Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
            menuPage.Menu.SelectedItem = null;
            IsPresented = false;
        }

        public static void ActionForm(bool value)
        {
            ImageSource img = null;
            if (CacheHelper.ContainsKey("Photo"))
            {
                img = (ImageSource)CacheHelper.GetValue("Photo");
                MenuPage.stacklayout.Children.Add(new Image
                {
                    Source = img,
                    WidthRequest = 200,
                    HeightRequest = 200,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(10, 0)
                });
            }

            MenuPage.stacklayout.Children.Add(new Label { TextColor = Color.Black, Text = (string)CacheHelper.GetValue("Name"), HorizontalTextAlignment = TextAlignment.Center });
            MenuPage.stacklayout.Children.Add(new Label { TextColor = Color.Black, Text = "MENU" });

            Page displayPage = new TabbedPage1();
            displayPage.Title = "ITM Proyect";
            GlobalData.MasterDetailPage.IsGestureEnabled = true;
            GlobalData.MasterDetailPage.Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
        }
    }
}