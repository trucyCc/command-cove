using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using command_cove.Models;
using ReactiveUI;

namespace command_cove.ViewModels;

/*
 *
 * 文件夹视图Model
 *
 * @Description:
 * @Date: 2024年05月03日 星期五 19:54:36
 * @Author: Trucy
 * @Modify:
 */
public class FolderViewModel : ViewModelBase
{
    /// <summary>
    /// 当前选中的节点
    /// </summary>
    private Folder _selectedNode;

    public FolderViewModel()
    {
        // 初始化数据源
        using var db = new DatabaseManager();
        if (db.Folders.ToList().Count == 0)
        {
            Folder.InitData(db);
        }
        
        
        // Folders = BuildTree(GetRawDataFromDatabaseOrOtherSource());
        Folders = BuildTree(db.Folders.ToList());
        
        // 初始化选中节点，默认为空
        _selectedNode = new Folder();
    }
    
    

    /// <summary>
    /// 文件夹树
    /// </summary>
    public ObservableCollection<Folder> Folders { get; set; }

    /// <summary>
    /// 当前选中的节点
    /// </summary>
    public Folder SelectedNode
    {
        get => _selectedNode;
        set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
    }

    /// <summary>
    /// 构建文件树
    /// </summary>
    /// <param name="rawData">文件夹列表</param>
    /// <returns>文件树列表</returns>
    private ObservableCollection<Folder> BuildTree(List<Folder> rawData)
    {
        // 创建一个字典，用于快速查找节点
        var nodeDict = rawData.ToDictionary(node => node.Id);

        // 创建一个集合，用于存储顶级节点
        var tree = new ObservableCollection<Folder>();

        foreach (var node in rawData)
            // 如果当前节点是顶级节点，直接添加到树中
            if (node.ParentId == 0)
            {
                tree.Add(node);
            }
            else
            {
                // 如果当前节点有父节点，将其添加到父节点的 Children 集合中
                if (nodeDict.ContainsKey(node.ParentId)) nodeDict[node.ParentId].Children.Add(node);
            }

        return tree;
    }

    /// <summary>
    /// 模拟数据
    /// </summary>
    /// <returns>文件夹列表模拟数据</returns>
    private List<Folder> GetRawDataFromDatabaseOrOtherSource()
    {
        // 从数据源获取原始数据的逻辑
        // 这里返回一个示例数据作为演示
        return new List<Folder>
        {
            new()
            {
                Id = 1, Name = "根节点", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 41, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 51, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 61, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 71, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 81, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 91, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 121, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 131, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 141, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 151, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 161, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 1241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 2241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 3241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 4241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 5241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 6241, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 2741, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 2841, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 2941, Name = "根节点32", ParentId = 0, Type = 0,
                CreationTime = DateTime.Now
            },

            new()
            {
                Id = 2, Name = "子节点 2", ParentId = 1, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 3, Name = "子节点3 ", ParentId = 1, Type = 1,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 4, Name = "子节点 4", ParentId = 1, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 5, Name = "子节点 5", ParentId = 2, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 6, Name = "子节点 6", ParentId = 3, Type = 0,
                CreationTime = DateTime.Now
            },
            new()
            {
                Id = 7, Name = "子节点 7", ParentId = 2, Type = 1,
                CreationTime = DateTime.Now
            }
            // 其他节点...
        };
    }
}