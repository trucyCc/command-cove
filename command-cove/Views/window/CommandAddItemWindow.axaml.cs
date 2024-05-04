using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using command_cove.Models;

namespace command_cove.Views.window;

public partial class CommandAddItemWindow : Window
{
    /// <summary>
    /// 注释输入框
    /// </summary>
    private TextBox _commentTextBox;

    /// <summary>
    /// command数据
    /// </summary>
    private TextBox _commandStr;

    /// <summary>
    /// 排序
    /// </summary>
    private NumericUpDown _sortNumericUpDown;


    /// <summary>
    /// 输入事件
    /// </summary>
    public event EventHandler<Command> InputDataEntered;

    public CommandAddItemWindow()
    {
        InitializeComponent();

        // 失去焦点关闭窗口
        this.Deactivated += CommandAddItemWindow_Deactivated;
        // 按下回车关闭窗口
        this.Activated += CommandAddItemWindow_Activated;

        // 组件初始化
        _commentTextBox = this.FindControl<TextBox>("InputComment");
        _commandStr = this.FindControl<TextBox>("InputCommand");
        _sortNumericUpDown = this.FindControl<NumericUpDown>("InputSort");
    }

    /// <summary>
    /// 窗口失去焦点，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CommandAddItemWindow_Deactivated(object? sender, EventArgs e)
    {
        // 当窗口失去焦点时关闭窗口
        this.Close();
    }

    /// <summary>
    /// 按下回车，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CommandAddItemWindow_Activated(object? sender, EventArgs e)
    {
        this.KeyDown += CommandAddItemWindow_KeyDown;
    }

    /// <summary>
    /// 按下回车，关闭窗口，发送数据提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CommandAddItemWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var command = new Command()
            {
                CommandStr = _commandStr.Text,
                Comment = _commentTextBox.Text,
                CreationTime = DateTime.Now,
                Sort = (int) _sortNumericUpDown.Value
            };
            // 触发事件并传递输入框中的数据
            InputDataEntered?.Invoke(this, command);

            // 关闭弹窗
            this.Close();
        }
    }

    private void InputSort_TextInput(object sender, TextInputEventArgs e)
    {
        if (!char.IsDigit(e.Text[0]) && e.Text[0] != '-' && e.Text[0] != '+')
        {
            e.Handled = true; // 阻止非数字字符输入
        }
    }
}