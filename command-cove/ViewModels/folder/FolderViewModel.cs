using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using command_cove.Models;
using command_cove.Models.compare;
using DynamicData;
using Microsoft.EntityFrameworkCore;
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
public class FolderViewModel : ViewModelBase, INotifyPropertyChanged
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
    private ObservableCollection<Folder> _folders;

    public ObservableCollection<Folder> Folders
    {
        get { return _folders; }
        set
        {
            if (_folders != value)
            {
                _folders = value;
                OnPropertyChanged(nameof(Folders));
            }
        }
    }

    // INotifyPropertyChanged 接口实现
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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
    private ObservableCollection<Folder> BuildTree(List<Folder> listData)
    {
        // 将 folders 列表序列化为 JSON 字符串
        string foldersJson = JsonSerializer.Serialize(listData);

        // 将 JSON 字符串反序列化为新的对象列表
        var rawData = JsonSerializer.Deserialize<List<Folder>>(foldersJson);


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
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        if (SelectedNode == null)
        {
            return;
        }

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

        // 刷新树
        refreshFolderList();
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

        // 刷新树
        refreshFolderList();
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

        // 刷新树
        refreshFolderList();
    }

    /// <summary>
    /// 删除指定文件夹
    /// </summary>
    /// <param name="folder"></param>
    public void RemoveSelectedItem()
    {
        var folderToDelete = _db.Folders.FirstOrDefault(f => f.Id == SelectedNode.Id);
        if (folderToDelete != null)
        {
            _db.Folders.Remove(folderToDelete);
            _db.SaveChanges();
        }

        // 刷新树
        refreshFolderList();
    }

    public void refreshFolderList()
    {
        // 更新视图
        var folders = _db.Folders.ToList();
        var observableCollection = BuildTree(folders);
        CompareAndUpdate(observableCollection);
    }

    /// <summary>
    /// 更新树状目录
    /// </summary>
    /// <param name="newFolders"></param>
    private void CompareAndUpdate(ObservableCollection<Folder> newFolders)
    {
        // 比较根文件夹并执行相应的操作
        var addedFolders = newFolders.Except(Folders, new FolderIdEqualityComparer()).ToList();
        var removedFolders = Folders.Except(newFolders, new FolderIdEqualityComparer()).ToList();

        foreach (var folder in addedFolders)
        {
            Folders.Add(folder);
        }

        foreach (var folder in removedFolders)
        {
            Folders.Remove(folder);
        }

        // 比较和更新子文件夹
        foreach (var folder in Folders)
        {
            var correspondingNewFolder = newFolders.FirstOrDefault(f => f.Id == folder.Id);
            if (correspondingNewFolder != null)
            {
                // 调用递归辅助方法
                CompareAndUpdateChildren(folder.Children, correspondingNewFolder.Children);
            }
        }
    }

    // 递归辅助方法
    private void CompareAndUpdateChildren(ObservableCollection<Folder> currentChildren,
        ObservableCollection<Folder> newChildren)
    {
        var addedChildren = newChildren.Except(currentChildren, new FolderIdEqualityComparer()).ToList();
        var removedChildren = currentChildren.Except(newChildren, new FolderIdEqualityComparer()).ToList();

        foreach (var child in addedChildren)
        {
            currentChildren.Add(child);
        }

        foreach (var child in removedChildren)
        {
            currentChildren.Remove(child);
        }

        // 递归比较和更新子文件夹
        foreach (var child in currentChildren)
        {
            var correspondingNewChild = newChildren.FirstOrDefault(c => c.Id == child.Id);
            if (correspondingNewChild != null)
            {
                CompareAndUpdateChildren(child.Children, correspondingNewChild.Children);
            }
        }
    }
}