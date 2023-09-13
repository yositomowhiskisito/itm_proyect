using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using XAM_ProyectITM.Core;
using XAM_ProyectITM.Droid.Core;
using Xamarin.Forms;

namespace XAM_ProyectITM.Droid
{
    [Activity(Label = "Proyect ITM", Icon = "@drawable/ITM", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            //Window.SetNavigationBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            Window.SetNavigationBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossFilePicker.Current = new FilePickerImplementation();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}