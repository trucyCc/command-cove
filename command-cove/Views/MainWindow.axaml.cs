using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using command_cove.Models;
using command_cove.ViewModels;

namespace command_cove.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
    }

    private void Splitter_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is GridSplitter splitter) splitter.Background = Brushes.RoyalBlue;
    }

    private void Splitter_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is GridSplitter splitter) splitter.Background = Brushes.Transparent;
    }

    private void TreeView_SelectedItemChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!(sender is TreeView treeView))
            return;

        if (!(treeView.SelectedItem is Folder selectedCategory))
            return;

        if (!(selectedCategory is {Type: 1}))
            return;

        // 当 selectedCategory 不为 null 且 Type 属性等于 1 时执行
        selectedCategory.Name = $"{selectedCategory.Name}_{selectedCategory.Type}";
    }
}