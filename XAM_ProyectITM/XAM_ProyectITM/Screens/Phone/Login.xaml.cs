using LIBDomainEntities.Entities;
using LIBPresentationContext.Implementations.VwModels;
using LIBPresentationCore.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

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
                TextChanged(null, null);
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
                this.frimdel.BackgroundColor = Color.DeepSkyBlue;
                this.txUser.Text = string.Empty;
                this.txPassword.Text = string.Empty;
                this.frimdel.BackgroundColor = Color.White;
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
                this.frEnt.BackgroundColor = Color.DeepSkyBlue;
                this.frEnt.HasShadow = false;
                var data = new Dictionary<string, object>();
                data["Email"] = this.txUser.Text;
                data["Password"] = this.txPassword.Text;

                var response = await VwModel.Login(data);

                this.frEnt.HasShadow = true;
                this.frEnt.BackgroundColor = Color.White;

                if (response.ContainsKey("Error"))
                {
                    btClean_Click(null, null);
                    this.frEnt.HasShadow = true;
                    this.frEnt.BackgroundColor = Color.White;
                    return;
                }

                ImageSource imgsrc = null;
                byte[] bArray = ((Users)response["User"])._Person.File;

                imgsrc = ImageSource.FromStream(() =>
                {
                    var ms = new MemoryStream(bArray);
                    ms.Position = 0;
                    return ms;
                });

                string user  = ((Users)response["User"])._Person.Name;
                CacheHelper.Add("Name", user);
                CacheHelper.Add("Photo", imgsrc);
                MainWindow.ActionForm(true);
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
                if (this.txPassword.Text == string.Empty || this.txPassword.Text == null)
                    this.txPassword.BackgroundColor = Color.Pink;
                else
                    this.txPassword.BackgroundColor = Color.LightGray;

                if (this.txUser.Text == string.Empty || this.txUser.Text == null)
                    this.txUser.BackgroundColor = Color.Pink;
                else
                this.txUser.BackgroundColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new SCNPersons());
                //await Navigation.PushModalAsync(new SCNPersons()); --Abre en modo modal
            }
            catch (Exception ex)
            {
                LogsHelper.Logs(ex);
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");

                mail.From = new MailAddress("alejandro_albanes85@hotmail.com");
                mail.To.Add("alejandro_albanes85@hotmail.com");
                mail.Subject = "Subject Test";
                mail.Body = "Body test";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.live.com";
                SmtpServer.EnableSsl = false;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("alejandro_albanes85@hotmail.com", "abc123-!!");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                DisplayAlert("Send","Email was send", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}