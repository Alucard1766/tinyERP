using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    class LevelToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const int oneLevelMargin = 10;
            int margin = (value != null) ? oneLevelMargin : 0;
            return new Thickness(margin, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
