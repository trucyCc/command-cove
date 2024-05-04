﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


namespace command_cove.Models;

/*
 *
 * 文件夹结构,结构树
 *
 * @Description:
 * @Date: 2024年04月27日 星期六 17:17:47
 * @Author: Trucy
 * @Modify:
 */
public class Folder : INotifyPropertyChanged
{
    /// <summary>
    /// 唯一键
    /// </summary>
    public int Id { get; init; }

    private String _name;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            // 属性发生变化，通知界面重新渲染
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    /// <summary>
    /// 父级Id
    /// </summary>
    public int ParentId { get; init; }

    /// <summary>
    /// 0 文件夹类型 1 指令集类型
    /// 如果是1点击后修改右侧Grid内容，展示指令集
    /// 如果是0点击后展开下一层
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; init; }

    /// <summary>
    /// 子级别文件夹
    /// </summary>
    [NotMapped]
    public List<Folder> Children { get; set; }

    /// <summary>
    /// 属性变化通知
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 属性变化通知方法
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static void InitData(DatabaseManager db)
    {
        if (db.Folders.Any())
            return;
        
        // 如果不存在，则添加默认文件夹
        var defaultFolder = new Folder
        {
            Name = "Default Folder",
            ParentId = 0, // 假设是顶级文件夹
            Type = 0, // 文件夹类型
            CreationTime = DateTime.Now
        };

        // 插入到数据库
        db.Folders.Add(defaultFolder);
        db.SaveChanges();
    }
}

// using System;
// using System.ComponentModel;
//
// public class Folder: INotifyPropertyChanged
// {
//     public int Id { get; set; }
//     private String _name;
//
//     /// <summary>
//     /// 名称
//     /// </summary>
//     public string Name
//     {
//         get => _name;
//         set
//         {
//             // 属性发生变化，通知界面重新渲染
//             if (_name != value)
//             {
//                 _name = value;
//                 OnPropertyChanged(nameof(Name));
//             }
//         }
//     }
//     public int? ParentId { get; set; }
//     public int Type { get; set; }
//     public DateTime CreationTime { get; set; }
//     public event PropertyChangedEventHandler? PropertyChanged;
//
//     protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
//     {
//         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//     }
//
//     protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
//     {
//         if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//         field = value;
//         OnPropertyChanged(propertyName);
//         return true;
//     }
// }