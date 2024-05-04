using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace command_cove.Views.window;

/*
* 
* RightPanel RightMenu 新增命令集
*
* @Description:
* @Date: 2024年05月04日 星期六 20:51:25
* @Author: Trucy
* @Modify:
*/
public partial class AddCommandSetNodeWindow : Window
{
    
    /// <summary>
    /// 输入框
    /// </summary>
    private TextBox _textBox;

    /// <summary>
    /// 输入事件
    /// </summary>
    public event EventHandler<string> InputDataEntered;

    public AddCommandSetNodeWindow()
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