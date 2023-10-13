using iText.Commons.Utils;
using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XAM_ProyectITM.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Action = LIBUtilities.Core.Action;

namespace XAM_ProyectITM.Screens.Phone
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SCNPersons : ContentPage, IUserControl
    {
        protected Action action;
        public IVmMaintenance<Persons> IVmMaintenance;

        public SCNPersons ()
		{
            InitializeComponent();

            cbState_CheckedChanged(null, null);

            var data = new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmPersons(data);
            this.BindingContext = IVmMaintenance;

            ((VmPersons)this.BindingContext).Current = new Persons();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

                LoadArrayByte(stream);
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }



        private async void LoadArrayByte(Stream stream)
        {
            try
            {
                byte[] byteArray;
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    byteArray = ms.ToArray();
                }

                if (((VmPersons)this.BindingContext).Current != null)
                    ((VmPersons)this.BindingContext).Current.File = byteArray;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void cbState_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            try
            {
                if (this.cbState.IsChecked == false)
                    this.cbState.Color = Color.Pink;
                else
                    this.cbState.Color = Color.DeepSkyBlue;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
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

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                ((VmPersons)this.BindingContext).Action = Action.NEW;
                await ((VmPersons)this.BindingContext).Save();
                this.imgUser.IsVisible = true;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SCNUsers(((VmPersons)this.BindingContext).Current));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}