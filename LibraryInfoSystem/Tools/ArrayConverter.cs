using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryInfoSystem.Tools
{
    public class ArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string[] array)
            {
                return string.Join(", ", array);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
