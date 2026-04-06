using System.Globalization;
using System.Windows.Data;

namespace FitnessTracker.Converters;

/// <summary>Returns true when active nav key equals the button's nav key (both strings).</summary>
public sealed class NavKeyActiveMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is not string active || values[1] is not string key)
            return false;
        return string.Equals(active, key, StringComparison.Ordinal);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
