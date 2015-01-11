using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ScreenRecorder.Converters
{
    class RecordStateToIconElementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var recording = value as bool?;

            if (recording != null)
                return recording == true ? new SymbolIcon(Symbol.Stop) : new SymbolIcon(Symbol.Play);
            return new SymbolIcon(Symbol.Stop);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
