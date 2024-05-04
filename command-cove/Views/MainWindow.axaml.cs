using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using command_cove.Models;
using command_cove.ViewModels;

namespace command_cove.Views;

/*
 *
 * 主窗口视图
 *
 * @Description:
 * @Date: 2024年05月04日 星期六 20:52:53
 * @Author: Trucy
 * @Modify:
 */
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 分割线 鼠标进入事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Splitter_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is GridSplitter splitter) splitter.Background = Brushes.RoyalBlue;
    }

    /// <summary>
    /// 分割线 鼠标离开事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Splitter_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is GridSplitter splitter) splitter.Background = Brushes.Transparent;
    }
}