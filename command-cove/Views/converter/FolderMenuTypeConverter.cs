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
            // 选项是文件夹类型，显示菜单项
            return folderType == 0 ? true : false;

        throw new NotImplementedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}