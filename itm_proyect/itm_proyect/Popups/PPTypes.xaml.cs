using LIBPresentationCore.Core;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using LIBPresentationContext.Implementations.VwModels;
using System.Collections.Generic;
using LIBDomainEntities.Entities;
using LIBUtilities.Core;

namespace XAMBase_Proyect.Popups
{
    public partial class PPTypes : PopupPage, IUCPopup
    {
        public IVmMaintenance<Persons> IVmMaintenance;
        public bool IsLoad = false;

        public PPTypes(Dictionary<string, object> data)
        {
            /*InitializeComponent();

            data = data ?? new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmUserTypes(data);
            this.BindingContext = IVmMaintenance;
            IVmMaintenancePersons = (VmPersons)data["DataBinding"];
            UserControl_Loaded();*/
        }

        private async void UserControl_Loaded()
        {
            try
            {
                if (IsLoad)
                    return;
                await IVmMaintenance.Load();
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
                /*var types = (UserTypes)Selected;
                if (types == null)
                    return;

                ((VmPersons)IVmMaintenancePersons).Current.EntUserTypes = types;
                ((VmPersons)IVmMaintenancePersons).Current.Type = types.Id;
                Button_Clicked(null, null);*/
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