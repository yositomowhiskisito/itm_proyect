using Android.Support.V7.App;
using XAM_ProyectITM.Droid.Services;
using XAM_ProyectITM.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(StyleService))]
namespace XAM_ProyectITM.Droid.Services
{
    public class StyleService : IStyleService
    {
        public void GetStyle(string value)
        {
            if (value == "Dark")
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
            else
                AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
        }
    }
}