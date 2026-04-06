using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FitnessTracker.Converters;

public sealed class HexToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string s || s.Length < 4)
            return new SolidColorBrush(Color.FromRgb(0xF8, 0xFA, 0xFC));
        try
        {
            var color = (Color)ColorConverter.ConvertFromString(s)!;
            return new SolidColorBrush(color);
        }
        catch
        {
            return new SolidColorBrush(Color.FromRgb(0xF8, 0xFA, 0xFC));
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
