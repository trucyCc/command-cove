using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace command_cove.Models;

/*
 *
 * 命令行
 *
 * @Description:
 * @Date: 2024年04月27日 星期六 17:17:20
 * @Author: Trucy
 * @Modify:
 */
public class Command : INotifyPropertyChanged
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// 所属命令集ID
    /// </summary>
    public int CommandSetId { get; set; }

    private string? _commandStr;

    /// <summary>
    /// 指令 str
    /// </summary>
    public string? CommandStr
    {
        get => _commandStr;
        set
        {
            // 检查属性是否有变化
            if (_commandStr == value)
                return;

            // 属性变化通知
            _commandStr = value;
            OnPropertyChanged(nameof(CommandStr));
        }
    }

    /// <summary>
    /// 注释
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// 补充备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreationTime { get; set; }

    private int _sort = 0;
    
    /// <summary>
    /// 在命令集中的顺序
    /// </summary>
    public int Sort
    {
        get => _sort;
        set
        {
            // 检查属性是否有变化
            if (_sort == value)
                return;

            // 属性变化通知
            _sort = value;
            OnPropertyChanged(nameof(Sort));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}