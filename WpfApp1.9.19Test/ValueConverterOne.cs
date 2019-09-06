using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1._9._19Test
{
    class ValueConverterOne : IValueConverter
    {
        int minimumSize = 110;
        int maximumSize = 220;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int SizeOfSelection = System.Convert.ToInt32(value);

            if (SizeOfSelection <= minimumSize + 25)
            {
                return "SMALL";
            }
            else if (SizeOfSelection <= minimumSize + 50 )
            {
                return "MEDIUM";
            }
            else if (SizeOfSelection <= minimumSize + 75)
            {
                return "LARGE";
            }
            else if (SizeOfSelection <= maximumSize)
            {
                return "EXTRA LARGE";
            }
            else
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
