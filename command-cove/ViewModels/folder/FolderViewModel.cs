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

    /// <summary>
    /// 数据库连接
    /// </summary>
    private DatabaseManager _db;

    public FolderViewModel()
    {
        // 检查是否有树状结构
        _db = new DatabaseManager();
        if (!_db.Folders.Any())
        {
            Folder.InitData(_db);
        }
        
        // 从数据库加载数据初始化列表
        Folders = new ObservableCollection<Folder>(BuildTree(_db.Folders.ToList()));
        
        // 初始化选中节点，默认为空
        SelectedNode = Folders.FirstOrDefault()!;
        MessageBus.Current.SendMessage(SelectedNode);
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
                if (nodeDict.ContainsKey(node.ParentId)) nodeDict[node.ParentId]?.Children?.Add(node);
            }

        return tree;
    }

    /// <summary>
    /// 新增同级节点
    /// </summary>
    public void AddLevelNode(string name)
    {
        var folder = new Folder
        {
            Name = name,
            ParentId = SelectedNode.ParentId, // 同级别
            Type = SelectedNode.Type, // 文件夹类型
            CreationTime = DateTime.Now
        };

        // 插入到数据库
        _db.Folders.Add(folder);
        _db.SaveChanges();

        // 更新视图
        var folders = new List<Folder>(Folders);
        folders.Add(folder);
        var observableCollection = BuildTree(folders);
        Folders.Clear();
        foreach (var folderItem in observableCollection)
        {
            Folders.Add(folderItem);
        }
    }

    /// <summary>
    /// 新增当前子节点
    /// </summary>
    /// <param name="name"></param>
    public void AddChildNode(string name)
    {
        // 如果不存在，则添加默认文件夹
        var folder = new Folder
        {
            Name = name,
            ParentId = SelectedNode.Id, // 同级别
            Type = SelectedNode.Type, // 文件夹类型
            CreationTime = DateTime.Now
        };

        // 插入到数据库
        _db.Folders.Add(folder);
        _db.SaveChanges();

        // 更新视图
        var folders = new List<Folder>(Folders);
        folders.Add(folder);
        var observableCollection = BuildTree(folders);
        Folders.Clear();
        foreach (var folderItem in observableCollection)
        {
            Folders.Add(folderItem);
        }
    }

    /// <summary>
    /// 新增命令集节点
    /// </summary>
    /// <param name="name"></param>
    public void AddCommandSetNode(string name)
    {
        // 如果不存在，则添加默认文件夹
        var folder = new Folder
        {
            Name = name,
            ParentId = SelectedNode.Id, // 同级别
            Type = 1, // 指令集类型
            CreationTime = DateTime.Now
        };

        // 插入到数据库
        _db.Folders.Add(folder);
        _db.SaveChanges();

        // 更新视图
        var folders = new List<Folder>(Folders);
        folders.Add(folder);
        var observableCollection = BuildTree(folders);
        Folders.Clear();
        foreach (var folderItem in observableCollection)
        {
            Folders.Add(folderItem);
        }
    }
}