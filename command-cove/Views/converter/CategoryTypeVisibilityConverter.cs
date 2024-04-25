using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace command_cove.Views.converter;

public class CategoryTypeVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int categoryType)
        {
            return categoryType == 0 ? true : false;
        }

        throw new NotImplementedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}