using System;
using System.Globalization;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            double number;
            if (double.TryParse(value as string, out number))
            {
                return number;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : value.ToString();
        }
    }
}