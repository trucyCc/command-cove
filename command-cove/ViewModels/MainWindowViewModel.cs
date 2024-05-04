using System;
using command_cove.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace command_cove.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private Folder _selectedNode;

    public Folder SelectedNode
    {
        get => _selectedNode;
        set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
    }

    public MainWindowViewModel()
    {
        // 订阅 FolderViewModel 的 SelectedNode 属性的变化
        MessageBus.Current.Listen<Folder>()
            .Subscribe(folder =>
            {
                SelectedNode = folder;
                // 在这里执行其他你想要的逻辑，比如更新 UI
            });
    }
}