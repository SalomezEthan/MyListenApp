using Microsoft.UI.Xaml.Data;
using System;

namespace MyListenApp.Converters
{
    internal sealed partial class DurationToSmoothDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan duration)
            {
                var seconds = (int)duration.TotalSeconds;
                return TimeSpan.FromSeconds(seconds);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
