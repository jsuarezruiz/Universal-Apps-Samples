using System;
using Windows.UI.Xaml.Data;

namespace ScreenRecorder.Converters
{
    public class RecordStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var recording = value as bool?;

            if (recording != null)
                return recording == true ? "Stop" : "Play";
            return "Play";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
