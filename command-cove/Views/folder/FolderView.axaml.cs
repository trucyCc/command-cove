using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using command_cove.Models;
using command_cove.ViewModels;

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
}