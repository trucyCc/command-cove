using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using command_cove.Models;
using command_cove.ViewModels.panel.command;
using command_cove.Views.window;

namespace command_cove.Views.panel.command;

/*
 *
 * 命令列表Panel
 *
 * @Description:
 * @Date: 2024年05月04日 星期六 20:50:15
 * @Author: Trucy
 * @Modify:
 */
public partial class CommandPanelView : UserControl
{
    public CommandPanelView()
    {
        InitializeComponent();
        DataContext = new CommandPanelViewModel();
    }

    /// <summary>
    /// 打开 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OpenDialog_CommandAddItemWindow(object? sender, RoutedEventArgs e)
    {
        var commandAddItemWindow = new CommandAddItemWindow();
        commandAddItemWindow.InputDataEntered += CommandAddItemWindow_InputDataEntered;
        commandAddItemWindow.Show();
    }

    /// <summary>
    /// 新增子节点窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="inputData"></param>
    private void CommandAddItemWindow_InputDataEntered(object? sender, Command inputData)
    {
        if (DataContext is CommandPanelViewModel model)
        {
            model.AddCommand(inputData);
        }
    }

    private void RemoveItem(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button {DataContext: Command command})
        {
            return;
        }

        if (DataContext is not CommandPanelViewModel model)
        {
            return;
        }

        model.RemoveCommand(command);
    }
}