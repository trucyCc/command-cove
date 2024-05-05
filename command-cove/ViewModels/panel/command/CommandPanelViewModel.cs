using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using command_cove.Models;
using ReactiveUI;

namespace command_cove.ViewModels.panel.command;

/*
 *
 * 命令板实现
 *
 * @Description:
 * @Date: 2024年05月04日 星期六 20:43:45
 * @Author: Trucy
 * @Modify:
 */
public class CommandPanelViewModel : ViewModelBase
{
    /// <summary>
    /// 数据库
    /// </summary>
    private DatabaseManager _db;

    /// <summary>
    /// 当前选中的 命令集ID
    /// </summary>
    private int _commandSetId = 1;

    /// <summary>
    /// 当前选中节点
    /// </summary>
    private Folder? _selectedNode;

    /// <summary>
    /// 当前选中节点
    /// </summary>
    public Folder? SelectedNode
    {
        get => _selectedNode;
        set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
    }

    /// <summary>
    /// 文件夹树
    /// </summary>
    public ObservableCollection<Command> Commands { get; set; }

    public CommandPanelViewModel()
    {
        // 初始化数据
        _db = new DatabaseManager();
        Commands = new ObservableCollection<Command>();

        // 订阅 FolderViewModel 的 SelectedNode 属性的变化
        MessageBus.Current.Listen<Folder>()
            .Subscribe(folder =>
            {
                SelectedNode = folder;
                if (SelectedNode != null)
                {
                    // 根据selected获取对应的数据
                    _commandSetId = SelectedNode.Id;
                    // 从数据库读取数据
                    var commands = _db.Commands
                        .Where(command => command.CommandSetId == SelectedNode.Id)
                        .Where(command => command.IsDelete == false).ToList();
                    // 调整命令排序
                    commands = commands.OrderBy(c => c.Sort).ToList();
                    // 刷新视图
                    Commands.Clear();
                    foreach (var command in commands)
                    {
                        Commands.Add(command);
                    }
                }
            });
    }

    /// <summary>
    /// 添加命令
    /// </summary>
    /// <param name="inputData"></param>
    public void AddCommand(Command inputData)
    {
        inputData.CommandSetId = _commandSetId;

        // 插入到数据库
        _db.Commands.Add(inputData);
        _db.SaveChanges();

        // 更新视图
        var commands = new List<Command>(Commands);
        commands.Add(inputData);
        commands = commands.OrderBy(c => c.Sort).ToList();
        Commands.Clear();
        foreach (var command in commands)
        {
            Commands.Add(command);
        }
    }

    /// <summary>
    /// 删除指定命令
    /// </summary>
    /// <param name="command"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void RemoveCommand(Command command)
    {
        var commandToDelete = _db.Commands.FirstOrDefault(f => f.Id == command.Id);
        if (commandToDelete != null)
        {
            commandToDelete.IsDelete = true;
            commandToDelete.DeleteTime = DateTime.Now;
            _db.Commands.Update(commandToDelete);
            _db.SaveChanges();
        }

        var commands = _db.Commands
            .Where(c => c.CommandSetId == command.CommandSetId)
            .Where(c => c.IsDelete == false)
            .ToList();
        // 调整命令排序
        commands = commands.OrderBy(c => c.Sort).ToList();
        // 刷新视图
        Commands.Clear();
        foreach (var c in commands)
        {
            Commands.Add(c);
        }
    }
}