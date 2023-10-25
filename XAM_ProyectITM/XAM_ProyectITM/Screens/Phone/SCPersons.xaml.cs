using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XAM_ProyectITM.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using MenuItem = Xamarin.Forms.MenuItem;
using XAM_ProyectITM.Services;
using Plugin.XamarinFormsSaveOpenPDFPackage;
using LIBPresentationContext.Implementations.Helpers.PdfHelper;

namespace XAM_ProyectITM.Screens.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SCPersons : ContentPage, IUserControl
    {
        public IVmMaintenance<Persons> IVmMaintenance;
        private IUCPopup IUCPopup;

        public SCPersons()
        {
            try
            {
                InitializeComponent();

                ConfigColor();
                TextChanged(null, null);
                cbState_CheckedChanged(null, null);

                var data = new Dictionary<string, object>();
                data.Add("View", this);
                IVmMaintenance = new VmPersons(data);
                this.BindingContext = IVmMaintenance;
                UserControl_Loaded(null, null);
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void ConfigColor()
        {
            try
            {
                this.lbTitle.SetAppThemeColor(Label.TextColorProperty, Color.Black, Color.DeepSkyBlue);
                this.cpPersons.SetAppThemeColor(ContentPage.BackgroundColorProperty, Color.White, Color.Black);
                this.fmButtons.SetAppThemeColor(Frame.BackgroundColorProperty, Color.White, Color.DarkGray);
                this.fmDetails.SetAppThemeColor(Frame.BackgroundColorProperty, Color.White, Color.DarkGray);
                this.aiLoading.SetAppThemeColor(ActivityIndicator.ColorProperty, Color.Black, Color.DeepSkyBlue);
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
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

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

                LoadArrayByte(stream);

                /*if (stream != null)
                {
                    image.Source = ImageSource.FromStream(() => stream);
                }*/
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        public void Close(bool open = false)
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

        private async void UploadFile_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await CrossFilePicker.Current.PickFile("image/*");
                if (response == null)
                    return;
                var path = response.FilePath;
                var name = response.FileName;
                var array = response.DataArray;

                IVmMaintenance.Current.File = array;
                IVmMaintenance.Current.Name = name;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private async Task TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

                LoadArrayByte(stream);
                if (stream != null)
                {
                    image.Source = ImageSource.FromStream(() => stream);
                }
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

        public void Show() { }

        private void dgList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {

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
                var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "test.pdf");
                var helper = new HelperPdf();

                var docpdf = helper.CreatePdf2(((VmPersons)this.BindingContext).List, backingFile);

                await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("Persons.pdf", "application/pdf", docpdf, PDFOpenContext.InApp);
                
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }


        public async Task ActiveButtons(LIBUtilities.Core.Action temp)
        {
            try
            {

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

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var menuitem = sender as MenuItem;
                if (menuitem != null)
                {
                    var name = menuitem.Text as string;
                    switch(name)
                    {
                        case "Delete": await ((VmPersons)this.BindingContext).Delete(); break;
                        case "Edit": await ((VmPersons)this.BindingContext).Update(); break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (((CustomEntry)this.txName).Text == string.Empty || ((CustomEntry)this.txName).Text == null)
                {
                    ((CustomEntry)this.txName).BorderColor = Color.LightGray;
                    this.txName.BackgroundColor = Color.Pink;
                }
                if (this.txName.Text != string.Empty || this.txName.Text != null)
                    ((CustomEntry)this.txName).BackgroundColor = Color.Transparent;

                if (this.txSSN.Text == string.Empty || this.txSSN.Text == null)
                    this.txSSN.BackgroundColor = Color.Pink;
                else
                    this.txSSN.BackgroundColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void txBorn_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
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
    }
}