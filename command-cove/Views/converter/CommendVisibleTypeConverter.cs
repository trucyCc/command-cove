using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace command_cove.Views.converter;

public class CommendVisibleTypeConverter : IValueConverter
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    /// <param name="value">当前值</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int folderType)
            // 选项是命令
            return folderType == 1 ? true : false;

        throw new NotImplementedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}