using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    class BooleanToRevenueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            bool isRevenue = (bool) value;
            return isRevenue ? "Einnahme" : "Ausgabe";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string revenue = (string) value;
            return revenue == "Einnahme";
        }
    }
}
