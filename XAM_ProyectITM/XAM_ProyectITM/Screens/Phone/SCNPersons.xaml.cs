using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAM_ProyectITM.Screens.Phone
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SCNPersons : ContentPage, IUserControl
    {
        public IVmMaintenance<Persons> IVmMaintenance;
        public SCNPersons ()
		{
            InitializeComponent();

            cbState_CheckedChanged(null, null);

            var data = new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmPersons(data);
            this.BindingContext = IVmMaintenance;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void cbState_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }

        public void Close(bool noTab = true){ }

        public void MoveFocus(Focus focus = LIBPresentationCore.Core.Focus.FIRST){ }

        public async Task Loading(Loading action){ }

        public void Detail(bool open = false){ }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txBorn_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}