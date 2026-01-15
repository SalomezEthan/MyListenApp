using Microsoft.UI.Xaml.Data;
using System;

namespace MyListenApp.Converters
{
    internal sealed partial class BoolToPlayIcon : IValueConverter
    {
        const string PLAY_SOLID_ICON = "\uF5B0";
        const string PAUSE_SOLID_ICON = "\uF8AE";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is false) ? PLAY_SOLID_ICON : PAUSE_SOLID_ICON;  
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is PAUSE_SOLID_ICON;
        }
    }
}
