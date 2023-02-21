using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ControlTemplate
{
	public class TitleToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            System.Diagnostics.Debug.WriteLine("value "+value);
            System.Diagnostics.Debug.WriteLine("targetType " + targetType);
            System.Diagnostics.Debug.WriteLine("parameter " + parameter);
            System.Diagnostics.Debug.WriteLine("culture " + culture);


            if (value is string title && title == "json-server")
            {
                return "No";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}