using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace XAM_Proyect.Core
{
    public class BoolConverters: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return "Inactive";
                return value.ToString().ToUpper() == "TRUE" ?
                    "Active" :
                    "Inactive";
            }
            catch
            {
                return "Inactive";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                    return false;

                return value.ToString() == "X" ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class AgeConverters: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return value;
                int age = DateTime.Now.Year - ((DateTime)value).Year;
                DateTime currentDate = ((DateTime)value).AddYears(age);
                if (DateTime.Now.CompareTo(currentDate) < 0)
                    age--;
                return age;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ByteArrayToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            ImageSource imgsrc = null;
            try
            {
                if (value == null)
                    return null;
                byte[] bArray = (byte[])value;

                imgsrc = ImageSource.FromStream(() =>
                {
                    var ms = new MemoryStream(bArray);
                    ms.Position = 0;
                    return ms;
                });
            }
            catch (System.Exception sysExc)
            {
                System.Diagnostics.Debug.WriteLine(sysExc.Message);
            }
            return imgsrc;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
