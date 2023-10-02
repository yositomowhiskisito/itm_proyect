using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using XAM_ProyectITM.Screens.Phone;
using Xamarin.Forms;

namespace XAM_ProyectITM.Core
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }
        public static StackLayout stacklayout = new StackLayout();

        public MenuPage()
        {
            Title = "ITM Proyect Menu";
            BackgroundColor = Color.White;

            Menu = new MenuListView();

            stacklayout = new StackLayout();
            
            stacklayout.Children.Add(new Image
                {
                    Source = ImageSource.FromResource("XAM_ProyectITM.Images.ITM.png"),
                    WidthRequest = 35,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(10, 0)
                });
            

            //stacklayout.Children.Add(new Label { TextColor = Color.Black, Text = "MENU" });

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = stacklayout,
            };

            /*((StackLayout)menuLabel.Content).Children.Add(new Image
            {
                Source = ImageSource.FromResource("XAM_ProyectITM.Images.ITM.png"),
                WidthRequest = 35,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0)
            });
            ((StackLayout)menuLabel.Content).Children.Add(new Label
            {
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                Text = CacheHelper.GetValue("User").ToString()
            });*/

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
    }

    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = FillList();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.White;
            SeparatorVisibility = SeparatorVisibility.Default;

            var cell = new DataTemplate(typeof(MenuCell));
            cell.SetBinding(MenuCell.TextProperty, "Title");
            cell.SetBinding(MenuCell.ImageSourceProperty, "XAM_ProyectITM.Images.ITM.png");

            ItemTemplate = cell;
        }

        private List<MenuItem> FillList()
        {
            var list = new List<MenuItem>();

            list.Add(new MenuItem() { Title = "Home", TargetType = typeof(TabbedPage1) });
            list.Add(new MenuItem() { Title = "Persons", TargetType = typeof(SCPersons)});
            list.Add(new MenuItem() { Title = "Users", TargetType = typeof(SCUsers)  });
            return list;
        }
    }

    public class MenuCell : ImageCell
    {
        public MenuCell() : base()
        {
            this.TextColor = Color.Black;
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}