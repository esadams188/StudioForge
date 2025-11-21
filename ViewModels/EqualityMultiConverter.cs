using System;
using System.Globalization;
using System.Windows.Data;

namespace StudioForge.ViewModels
{
    public class EqualityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Length < 2)
            {
                return false;
            }

            var left = values[0];
            var right = values[1];
            return left != null && left.Equals(right);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
