using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    internal class SelectedTabToActiveButtonMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length != 2)
            {
                return DependencyProperty.UnsetValue;
            }

            if (string.IsNullOrEmpty(values[0].ToString()) || string.IsNullOrEmpty(values[1].ToString()))
            {
                return DependencyProperty.UnsetValue;
            }

            int buttonIndex = int.Parse(values[0].ToString());
            int selectedTab = int.Parse(values[1].ToString());

            return selectedTab != buttonIndex;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
