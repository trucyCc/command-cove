using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Threading;
using command_cove.Models;
using ReactiveUI;

namespace command_cove.ViewModels.panel.command;

public class CommandPanelViewModel : ViewModelBase
{
    private DatabaseManager _db;
    private int _commandSetId = 1;

    private Folder _selectedNode;

    public Folder SelectedNode
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
        _db = new DatabaseManager();
        Commands = new ObservableCollection<Command>();

        // 订阅 FolderViewModel 的 SelectedNode 属性的变化
        MessageBus.Current.Listen<Folder>()
            .Subscribe(folder =>
            {
                SelectedNode = folder;
                // 根据selected获取对应的数据
                _commandSetId = SelectedNode.Id;
                var commands = _db.Commands.Where(command => command.CommandSetId == SelectedNode.Id).ToList();
                Commands.Clear();
                foreach (var command in commands)
                {
                    Commands.Add(command);
                }
            });
    }

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
}