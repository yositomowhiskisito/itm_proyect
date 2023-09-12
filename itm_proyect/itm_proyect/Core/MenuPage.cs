﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using itm_proyect.Screens.Phone;

namespace itm_proyect.Core
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Title = "Menu";
            BackgroundColor = Color.White;

            Menu = new MenuListView();
            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label { TextColor = Color.Black, Text = "MENU" }
            };

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
            BackgroundColor = Color.Black;
            SeparatorVisibility = SeparatorVisibility.Default;

            var cell = new DataTemplate(typeof(MenuCell));
            cell.SetBinding(MenuCell.TextProperty, "Title");
            cell.SetBinding(MenuCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }

        private List<MenuItem> FillList()
        {
            var list = new List<MenuItem>();
            list.Add(new MenuItem() { Title = "Persons", TargetType = typeof(ScPersons) });
            return list;
        }
    }

    public class MenuCell : ImageCell
    {
        public MenuCell() : base()
        {
            this.TextColor = Color.White;
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}