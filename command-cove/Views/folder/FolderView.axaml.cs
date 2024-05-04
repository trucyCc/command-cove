using System;
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
        
        // 窗口加载完成后，触发事件
        AttachedToVisualTree += FolderView_AttachedToVisualTree!;
    }
    
    /// <summary>
    /// 在窗口或控件被附加到可视树时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FolderView_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
    {
        // 发送消息到ViewModel
        if (DataContext is FolderViewModel model)
        {
            // 获取默认Selected，发送到消息总线
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

        // 触发消息总线，更新MainWindow Left Panel
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