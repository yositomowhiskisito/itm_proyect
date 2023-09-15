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
                this.aiLoading.IsRunning = true;
                await Navigation.PushAsync(new SCPersons());
                this.aiLoading.IsRunning = false;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }
    }
}