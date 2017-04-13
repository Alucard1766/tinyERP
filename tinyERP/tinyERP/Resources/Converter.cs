using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace tinyERP.UI.Resources
{
    class BooleanToEarningConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            bool isEarning = (bool) value;
            return isEarning ? "Einnahme" : "Ausgabe";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string earning = (string) value;
            return earning == "Einnahme";
        }
    }
}
