using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace command_cove.Views.window;

public partial class AddLevelNodeWindow : Window
{
    /// <summary>
    /// 输入框
    /// </summary>
    private TextBox _textBox;

    /// <summary>
    /// 输入事件
    /// </summary>
    public event EventHandler<string> InputDataEntered;

    public AddLevelNodeWindow()
    {
        InitializeComponent();

        // 失去焦点关闭窗口
        this.Deactivated += AddLevelNodeWindow_Deactivated;
        // 按下回车关闭窗口
        this.Activated += AddLevelNodeWindow_Activated;

        // 组件初始化
        _textBox = this.FindControl<TextBox>("InputTextBox");
    }

    /// <summary>
    /// 窗口失去焦点，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddLevelNodeWindow_Deactivated(object? sender, EventArgs e)
    {
        // 当窗口失去焦点时关闭窗口
        this.Close();
    }

    /// <summary>
    /// 按下回车，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddLevelNodeWindow_Activated(object? sender, EventArgs e)
    {
        this.KeyDown += AddLevelNodeWindow_KeyDown;
    }

    /// <summary>
    /// 按下回车，关闭窗口，发送数据提交事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddLevelNodeWindow_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            // 触发事件并传递输入框中的数据
            InputDataEntered?.Invoke(this, _textBox.Text);

            // 关闭弹窗
            this.Close();
        }
    }
}