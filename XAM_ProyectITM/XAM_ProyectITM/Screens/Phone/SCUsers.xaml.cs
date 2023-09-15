using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAM_ProyectITM.Screens.Phone.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAM_ProyectITM.Screens.Phone
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SCUsers : ContentPage, IUserControl
    {
        public IVmMaintenance<Users> IVmMaintenance;
        private IUCPopup IUCPopup;

        public SCUsers ()
		{
			InitializeComponent (); var data = new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmUsers(data);
            this.BindingContext = IVmMaintenance;
            UserControl_Loaded(null, null);
        }

        private async void UserControl_Loaded(object p1, object p2)
        {
            try
            {
                await IVmMaintenance.Load();
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        public void Close(bool noTab = true)
        {
            try
            {
                this.clDetail.Width = new GridLength(0);
                this.clList.Width = new GridLength(1, GridUnitType.Star);
                this.grDetail.IsVisible = false;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public void MoveFocus(Focus focus = LIBPresentationCore.Core.Focus.FIRST)
        {
        }

        public async Task Loading(Loading action)
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

        public void Detail(bool open = false)
        {
            try
            {
                if (open)
                {
                    this.grDetail.IsVisible = true;
                    this.clList.Width = new GridLength(0);
                    this.clDetail.Width = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    this.clDetail.Width = new GridLength(0);
                    this.clList.Width = new GridLength(1, GridUnitType.Star);
                    this.grDetail.IsVisible = false;
                    this.imgnew.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                var data = new Dictionary<string, object>();
                data["DataBinding"] = this.BindingContext;
                IUCPopup = new PPPersons(data);
                ((PopupPage)IUCPopup).BackgroundColor = Color.FromRgba(0, 0, 0, 0.7);
                ((PopupPage)IUCPopup).CloseWhenBackgroundIsClicked = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        await PopupNavigation.Instance.PushAsync((PopupPage)IUCPopup, false);
                    }
                    catch (Exception ex)
                    {
                        LogsHelper.Logs(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}