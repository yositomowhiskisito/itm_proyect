using System;
using Xamarin.Forms;
using itm_proyect.Core;

namespace itm_proyect.Screens.Phone
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

            Page displayPage = (Page)new ScPersons();
            displayPage.Title = "Persons";
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
            displayPage.Title = "Persons";
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

        internal static void ActionForm(bool value)
        {
            Page displayPage = new ScPersons();
            displayPage.Title = "Persons";
            GlobalData.MasterDetailPage.IsGestureEnabled = true;
            GlobalData.MasterDetailPage.Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
        }
    }
}