using System;
using System.Globalization;
using System.Windows.Data;

namespace VectorViewer.Converters
{
    /// <summary>
    /// Converter for multyplying doubles
    /// </summary>
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return AsDouble(value) * AsDouble(parameter);
        }

        double AsDouble(object value)
        {
            var valueText = value as string;
            if (valueText != null)
                return double.Parse(valueText, CultureInfo.InvariantCulture);
            else
                return (double)value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotSupportedException();
        }
    }
}
