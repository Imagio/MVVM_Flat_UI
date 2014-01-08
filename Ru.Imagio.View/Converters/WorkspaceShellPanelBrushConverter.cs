
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ru.Imagio.View.Converters
{
    public class WorkspaceShellPanelBrushConverter: IValueConverter
    {
        private static readonly List<Brush>  Brushes = new List<Brush>
        {
            new SolidColorBrush(Color.FromRgb(0x00, 0x72, 0xC6)),
            new SolidColorBrush(Color.FromRgb(0x2f, 0x96, 0xb4)),
            new SolidColorBrush(Color.FromRgb(0x51, 0xa3, 0x51)),
            new SolidColorBrush(Color.FromRgb(0xff, 0x8f, 0x32)),
            new SolidColorBrush(Color.FromRgb(0xbd, 0x36, 0x2f)),
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index;
            if (Int32.TryParse(value.ToString(), out index))
            {
                return Brushes[index % Brushes.Count];
            }
            return System.Windows.Media.Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
