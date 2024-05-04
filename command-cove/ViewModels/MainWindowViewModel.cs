using System;
using command_cove.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace command_cove.ViewModels;

/*
* 
* 主窗口实现
*
* @Description:
* @Date: 2024年05月04日 星期六 20:44:09
* @Author: Trucy
* @Modify:
*/
public class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// 当前选中节点
    /// </summary>
    private Folder _selectedNode;

    /// <summary>
    /// 当前选中节点
    /// </summary>
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
            });
    }
}