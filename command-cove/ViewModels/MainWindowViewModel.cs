using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Media;
using command_cove.Models;
using ReactiveUI;

namespace command_cove.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Category> Categories { get; set; }
    public Category SelectedNode { get; }

    public MainWindowViewModel()
    {
        SelectedNode = new Category();
        
        // 假设这里有一个从数据库或其他数据源获取的原始数据列表
        List<Category> rawData = GetRawDataFromDatabaseOrOtherSource();

        // 构建树形结构
        Categories = BuildTree(rawData);
        
    }

    private ObservableCollection<Category> BuildTree(List<Category> rawData)
    {
        // 创建一个字典，用于快速查找节点
        Dictionary<int, Category> nodeDict = rawData.ToDictionary(node => node.Id);

        // 创建一个集合，用于存储顶级节点
        ObservableCollection<Category> tree = new ObservableCollection<Category>();

        foreach (var node in rawData)
        {
            // 如果当前节点是顶级节点，直接添加到树中
            if (node.ParentId == 0)
            {
                tree.Add(node);
            }
            else
            {
                // 如果当前节点有父节点，将其添加到父节点的 Children 集合中
                if (nodeDict.ContainsKey(node.ParentId))
                {
                    nodeDict[node.ParentId].Children.Add(node);
                }
            }
        }

        return tree;
    }

    // 这里假设有一个方法用于从数据库或其他数据源获取原始数据
    private List<Category> GetRawDataFromDatabaseOrOtherSource()
    {
        // 从数据源获取原始数据的逻辑
        // 这里返回一个示例数据作为演示
        return new List<Category>
        {
            new Category
            {
                Id = 1, Name = "根节点", ParentId = 0, Type = 0, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 2, Name = "子节点 2", ParentId = 1, Type = 0, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 3, Name = "子节点3 ", ParentId = 1, Type = 1, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 4, Name = "子节点 4", ParentId = 1, Type = 0, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 5, Name = "子节点 5", ParentId = 2, Type = 0, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 6, Name = "子节点 6", ParentId = 3, Type = 0, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            new Category
            {
                Id = 7, Name = "子节点 7", ParentId = 2, Type = 1, CommandItems = new List<CommandItem>(),
                CreationTime = DateTime.Now
            },
            // 其他节点...
        };
    }
}