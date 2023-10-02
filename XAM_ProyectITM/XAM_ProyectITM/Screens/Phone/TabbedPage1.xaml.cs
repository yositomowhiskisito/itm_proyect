using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAM_ProyectITM.Screens.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void SendPersons_1(object sender, EventArgs e)
        {
            try
            {
                //await Navigation.PushAsync(new SCPersons());
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private async void SendUsers_1(object sender, EventArgs e)
        {
            try
            {
                //await Navigation.PushAsync(new SCUsers());
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (((ToolbarItem)sender).Text == "Ligth")
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                if (((ToolbarItem)sender).Text == "Dark")
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }
    }
}