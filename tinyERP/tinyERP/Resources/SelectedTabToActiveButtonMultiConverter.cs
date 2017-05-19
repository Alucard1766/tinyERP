using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    class SelectedTabToActiveButtonMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Count() != 2)
            {
                return DependencyProperty.UnsetValue;
            }
            if (string.IsNullOrEmpty(values[0].ToString()) || string.IsNullOrEmpty(values[1].ToString()))
            {
                return DependencyProperty.UnsetValue;
            }
            int buttonIndex = Int32.Parse(values[0].ToString());
            int selectedTab = Int32.Parse(values[1].ToString());
            if (selectedTab == buttonIndex)
            {
                return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
