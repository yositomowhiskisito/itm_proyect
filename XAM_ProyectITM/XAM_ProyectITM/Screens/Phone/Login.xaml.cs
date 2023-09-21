using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAM_ProyectITM.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAM_ProyectITM.Screens.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage, IUserControl
    {
        private VmUsers VwModel;

        public Login()
        {
            try
            {
                InitializeComponent();
                var data = new Dictionary<string, object>();
                data["Screen"] = this;
                VwModel = new VmUsers(data);
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public object User { get { return this.txUser.Text; } }
        public object Password { get { return this.txPassword.Text; } }

        public void Close(bool noTab = true){ }

        public void Detail(bool open = false){ }

        public async  Task Loading(Loading action)
        {
            try
            {
                if (action == LIBPresentationCore.Core.Loading.ADD)
                    this.aiLoading.IsRunning = true;
                else
                    this.aiLoading.IsRunning = false;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public void MoveFocus(Focus focus = LIBPresentationCore.Core.Focus.FIRST){ }

        private void btClean_Click(object sender, EventArgs e)
        {
            try
            {
                this.txUser.Text = string.Empty;
                this.txPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private async void btEnter_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new Dictionary<string, object>();
                data["Email"] = this.txUser.Text;
                data["Password"] = this.txPassword.Text;
                var response = await VwModel.Login(data);
                if (response.ContainsKey("Error"))
                {
                    //LogsHelper.Show(response["Error"].ToString());
                    return;
                }
                MainWindow.ActionForm(true);
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }
    }
}