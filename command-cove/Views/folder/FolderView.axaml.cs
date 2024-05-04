﻿using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using command_cove.Models;
using command_cove.ViewModels;
using command_cove.Views.window;
using ReactiveUI;
using Splat;

namespace command_cove.Views.folder;

/*
 *
 * 文件夹视图
 *
 * @Description:
 * @Date: 2024年05月03日 星期五 19:58:01
 * @Author: Trucy
 * @Modify:
 */
public partial class FolderView : UserControl
{
    public FolderView()
    {
        InitializeComponent();
        DataContext = new FolderViewModel(); // 确保FolderViewModel被实例化
        this.AttachedToVisualTree += FolderView_AttachedToVisualTree;
    }
    
    private void FolderView_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
    {
        // 发送消息到ViewModel
        var messageBus = Locator.Current.GetService<IMessageBus>();
        if (DataContext is FolderViewModel model)
        {
            MessageBus.Current.SendMessage(model.SelectedNode);
        }
        
    }

    /// <summary>
    /// 文件夹树，选中项事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TreeView_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!(sender is TreeView treeView))
            return;

        if (!(treeView.SelectedItem is Folder selectedFolder))
            return;

        // 文件夹项目不做操作
        // if (!(selectedFolder is {Type: 1}))
        //     return;

        // todo:根据节点类型，切换右侧视图内容
        MessageBus.Current.SendMessage(selectedFolder);
    }

    /// <summary>
    /// 打开 新增同级节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenDialog_AddLevelNode(object? sender, RoutedEventArgs e)
    {
        var addLevelNodeWindow = new AddLevelNodeWindow();
        addLevelNodeWindow.InputDataEntered += AddLevelNodeWindow_InputDataEntered;
        addLevelNodeWindow.Show();
    }

    /// <summary>
    /// 新增同级节点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="inputData"></param>
    private void AddLevelNodeWindow_InputDataEntered(object? sender, string inputData)
    {
        if (DataContext is FolderViewModel model)
        {
            model.AddLevelNode(inputData);
        }
    }
    
    /// <summary>
    /// 打开 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenDialog_AddChildFolderNode(object? sender, RoutedEventArgs e)
    {
        var addChildFolderNodeWindow = new AddChildFolderNodeWindow();
        addChildFolderNodeWindow.InputDataEntered += AddChildFolderNodeWindow_InputDataEntered;
        addChildFolderNodeWindow.Show();
    }

    /// <summary>
    /// 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="inputData"></param>
    private void AddChildFolderNodeWindow_InputDataEntered(object? sender, string inputData)
    {
        if (DataContext is FolderViewModel model)
        {
            model.AddChildNode(inputData);
        }
    }
    
    /// <summary>
    /// 打开 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenDialog_AddCommandSetNode(object? sender, RoutedEventArgs e)
    {
        var addCommandSetNodeWindow = new AddCommandSetNodeWindow();
        addCommandSetNodeWindow.InputDataEntered += AddCommandSetNodeWindow_InputDataEntered;
        addCommandSetNodeWindow.Show();
    }

    /// <summary>
    /// 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="inputData"></param>
    private void AddCommandSetNodeWindow_InputDataEntered(object? sender, string inputData)
    {
        if (DataContext is FolderViewModel model)
        {
            model.AddCommandSetNode(inputData);
        }
    }
}