using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    internal class BooleanToRevenueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;

            return (bool)value ? "Einnahme" : "Ausgabe";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;

            return (string)value == "Einnahme";
        }
    }
}
