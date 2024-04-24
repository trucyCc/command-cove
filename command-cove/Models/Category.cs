using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace command_cove.Models;

// 文件夹结构，包含Category 或者 Category Command
public class Category  : INotifyPropertyChanged
{
    // 名称
    private string _name;
    public string Name { get => _name; set
    {
        if (_name != value)
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    } }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // 唯一键
    public int Id { get; set; }

    // 父级Id
    public int ParentId { get; set; }

    // 0 文件夹类型 1 指令集类型
    // 如果是1点击后修改右侧Grid内容，展示指令集
    // 如果是0点击后展开下一层
    public int Type { get; set; }

    // 指令列表
    public List<CommandItem> CommandItems { get; set; }

    // 创建时间
    public DateTime CreationTime { get; set; }

    public List<Category> Children { get; set; }

    public Category(string name = null, int id = default, int parentId = default, int type = default, List<CommandItem>? commandItems = null, DateTime creationTime = default, List<Category>? children = null)
    {
        Name = name;
        Id = id;
        ParentId = parentId;
        Type = type;
        CommandItems = commandItems;
        CreationTime = creationTime;
        Children = children  ?? new List<Category>();
    }
}