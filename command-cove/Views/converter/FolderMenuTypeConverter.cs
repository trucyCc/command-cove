using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace command_cove.Views.converter;

/*
 *
 * Folder 右键菜单 Converter
 *
 * @Description:
 * @Date: 2024年04月27日 星期六 15:52:24
 * @Author: Trucy
 * @Modify:
 */
public class FolderMenuTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int categoryType)
            // 
            return categoryType == 0 ? true : false;

        throw new NotImplementedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}