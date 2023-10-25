using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using Action = LIBUtilities.Core.Action;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace XAM_ProyectITM.Screens.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SCNUsers : ContentPage, IUserControl
    {
        public IVmMaintenance<Users> IVmMaintenance;

        public SCNUsers(LIBDomainEntities.Entities.Persons current)
        {
            InitializeComponent();
            var data = new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmUsers(data);
            this.BindingContext = IVmMaintenance;

            ((VmUsers)this.BindingContext).Current = new Users();
            ((VmUsers)this.BindingContext).Current._Person = current;
            ((VmUsers)this.BindingContext).Current.Person = current.Id;

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var response = new Dictionary<string, object>();
            try
            {
                ((VmUsers)this.BindingContext).Current.Person = ((VmUsers)this.BindingContext).Current._Person.Id;
                ((VmUsers)this.BindingContext).Action = Action.NEW;
                response = await ((VmUsers)this.BindingContext).Save();

                await Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public void Close(bool noTab = true) { }

        public void MoveFocus(Focus focus = LIBPresentationCore.Core.Focus.FIRST) { }

        public async Task Loading(Loading action) { }

        public void Detail(bool open = false) { }
    }
}