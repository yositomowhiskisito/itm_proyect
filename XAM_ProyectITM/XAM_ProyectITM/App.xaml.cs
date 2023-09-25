using LIBUtilities.Core;
using System;
using Xamarin.Forms;
using XAM_ProyectITM.Core;

namespace XAM_ProyectITM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LIBCommunicationContext.Communication.Base = "192.168.100.5";
            
            MessagesHelper.IMessage = new MessageXAM();

            CreateCache();

            GlobalData.Application = this;
            MessagesHelper.SetIntance(new MessageXAM());

            if (Device.Idiom == TargetIdiom.Tablet)
                GlobalData.RightMenu = false;
            if (GlobalData.RightMenu)
                MainPage = new Screens.Phone.MainWindow();
        }

        private void CreateCache()
        {
            try
            {
                CacheHelper.Add("Type", false);
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}

