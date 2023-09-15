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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.IO;
using System.Reflection;
using Syncfusion.Drawing;
using MenuItem = Xamarin.Forms.MenuItem;

namespace XAM_ProyectITM.Screens.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SCPersons : ContentPage, IUserControl
    {
        public IVmMaintenance<Persons> IVmMaintenance;
        private IUCPopup IUCPopup;

        public SCPersons()
        {
            InitializeComponent();
            var data = new Dictionary<string, object>();
            data.Add("View", this);
            IVmMaintenance = new VmPersons(data);
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

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e) { }

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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await CrossFilePicker.Current.PickFile("images/*");
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
                /*var result = (VmPersons)this.BindingContext;
                var dic = JsonHelper.ConvertToObject(result.Current.ExtraInfo);
                result.Latitude = (string)dic["Latitude"];
                result.Longitude = (string)dic["Longitude"];

                this.grDetail.IsVisible = true;
                this.clList.Width = new GridLength(0);
                this.clDetail.Width = new GridLength(1, GridUnitType.Star);*/
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            try
            {
                //Create a new PDF document.
                PdfDocument doc = new PdfDocument();
                //Add a page.
                PdfPage page = doc.Pages.Add();
                //Create a PdfGrid.
                PdfGrid pdfGrid = new PdfGrid();
                //Add values to list.
                List<object> data = new List<object>();
                Object row1 = new { ID = "E01", Name = "Clay" };
                Object row2 = new { ID = "E02", Name = "Thomas" };
                Object row3 = new { ID = "E03", Name = "Andrew" };
                Object row4 = new { ID = "E04", Name = "Paul" };
                Object row5 = new { ID = "E05", Name = "Gray" };
                data.Add(row1);
                data.Add(row2);
                data.Add(row3);
                data.Add(row4);
                data.Add(row5);
                //Add list to IEnumerable.
                IEnumerable<object> dataTable = data;
                //Assign data source.
                pdfGrid.DataSource = dataTable;
                //Apply built-in table style
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                //Draw grid to the page of PDF document.
                pdfGrid.Draw(page, new PointF(10, 10));
                //Save the PDF document to stream.
                MemoryStream stream = new MemoryStream();
                doc.Save(stream);
                //Close the document.
                doc.Close(true);
                //Save the stream as a file in the device and invoke it for viewing
                //Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application/pdf", stream);
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
               
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var menuitem = sender as MenuItem;
                if (menuitem != null)
                {
                    var name = menuitem.Text as string;
                    var result = DisplayAlert("Alert", name, "Ok");
                }
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }
    }
}