using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
[PrimaryKey("Id")]
public class Folder(
    string name = "",
    int id = default,
    int parentId = default,
    int type = default,
    DateTime creationTime = default,
    List<Folder>? children = null)
    : INotifyPropertyChanged
{
    public Folder(string name, int id, int parentId, int type, DateTime creationTime)
        : this(name, id, parentId, type, creationTime, null)
    {
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get => name;
        set
        {
            // 属性发生变化，通知界面重新渲染
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
    
    /// <summary>
    /// 唯一键
    /// </summary>
    [Key]
    public int Id { get; init; } = id;

    /// <summary>
    /// 父级Id
    /// </summary>
    public int ParentId { get; init; } = parentId;

    /// <summary>
    /// 0 文件夹类型 1 指令集类型
    /// 如果是1点击后修改右侧Grid内容，展示指令集
    /// 如果是0点击后展开下一层
    /// </summary>
    public int Type { get; set; } = type;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; } = creationTime;

    /// <summary>
    /// 子级别文件夹
    /// </summary>
    public List<Folder> Children { get; set; } = children ?? new List<Folder>();

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
        var existingFolder = db.Folders.FirstOrDefault(f => f.Name == "Default Folder");
        if (existingFolder != null)
        {
            return;
        }

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