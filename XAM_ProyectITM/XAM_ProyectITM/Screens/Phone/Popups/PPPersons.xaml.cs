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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAM_ProyectITM.Screens.Phone.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PPPersons : PopupPage, IUCPopup
    {
        public IVmMaintenance<Users> IVmMaintenance;
        public IVmMaintenance<Persons> IVmMaintenancePersons;
        public bool IsLoad = false;

        public PPPersons(Dictionary<string, object> data)
        {
            InitializeComponent();

            data = data ?? new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenancePersons = new VmPersons(data);
            this.BindingContext = IVmMaintenancePersons;
            IVmMaintenance = (VmUsers)data["DataBinding"];
            UserControl_Loaded();
        }

        private async void UserControl_Loaded()
        {
            try
            {
                if (IsLoad)
                    return;
                await IVmMaintenancePersons.Load();
                IsLoad = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }


        public object Selected { get; set; }

        public void Close(bool noTab = true)
        {
            try
            {
                var types = (Persons)Selected;
                if (types == null)
                    return;

                ((VmUsers)IVmMaintenance).Current._Person = types;
                ((VmUsers)IVmMaintenance).Current.Person = types.Id;
                Button_Clicked(null, null);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void Detail(bool open = false)
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

        public void MoveFocus(Focus focus = LIBPresentationCore.Core.Focus.FIRST)
        {
        }

        public void Show()
        {
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.RemovePageAsync(this, false);
        }

        private void dgList_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

        }

        public async Task ActiveButtons(LIBUtilities.Core.Action temp)
        {
        }
    }
}