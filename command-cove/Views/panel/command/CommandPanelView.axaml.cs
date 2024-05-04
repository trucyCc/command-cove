using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using command_cove.Models;
using command_cove.ViewModels.panel.command;
using command_cove.Views.window;

namespace command_cove.Views.panel.command;

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
}