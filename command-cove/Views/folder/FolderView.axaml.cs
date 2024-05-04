using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using command_cove.Models;
using command_cove.ViewModels;
using command_cove.Views.window;

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

        if (!(treeView.SelectedItem is Folder selectedCategory))
            return;

        // 文件夹项目不做操作
        if (!(selectedCategory is {Type: 1}))
            return;

        // todo:根据节点类型，切换右侧视图内容

        // test:当 selectedCategory 不为 null 且 Type 属性等于 1 时执行
        selectedCategory.Name = $"{selectedCategory.Name}_{selectedCategory.Type}";
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
        // 在这里处理输入框中的数据
        Console.WriteLine("Input data entered: " + inputData);
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
        // 在这里处理输入框中的数据
        Console.WriteLine("Input data entered: " + inputData);
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
        // 在这里处理输入框中的数据
        Console.WriteLine("Input data entered: " + inputData);
    }
}