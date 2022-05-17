namespace WPFAnimation.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimeToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = System.Convert.ToDouble(value);
            var steps = System.Convert.ToDouble(parameter);

            return (time / steps) * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
