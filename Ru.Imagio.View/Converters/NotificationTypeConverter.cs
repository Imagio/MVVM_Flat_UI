using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Ru.Imagio.ViewModel.Notifications;

namespace Ru.Imagio.View.Converters
{
    public class NotificationTypeConverter : IValueConverter
    {
        private static readonly Dictionary<NotificationType, Brush> Settings = new Dictionary<NotificationType, Brush>
        {
            {NotificationType.Error, new SolidColorBrush(Color.FromRgb(0xe7, 0x4c, 0x3c))},
            {NotificationType.Warning, new SolidColorBrush(Color.FromRgb(0xf1, 0xc4, 0x0f))},
            {NotificationType.Notice, new SolidColorBrush(Color.FromRgb(0x34, 0x98, 0xdb))},
            {NotificationType.Success, new SolidColorBrush(Color.FromRgb(0x2e, 0xcc, 0x71))}
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Black;
            if (!(value is NotificationType))
                return Brushes.Black;
            if (!Settings.ContainsKey((NotificationType) value))
                return Brushes.Black;
            return Settings[(NotificationType) value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
