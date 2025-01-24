using System.Globalization;

namespace SubManager.App.Converters;

public class AvatarConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string path = value as string;
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            return "dotnet_bot.png"; 
        }
        return path;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}