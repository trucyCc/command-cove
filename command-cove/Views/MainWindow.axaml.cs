using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
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
        if (sender is GridSplitter splitter)
        {
            splitter.Background = Brushes.RoyalBlue;
        }
    }

    private void Splitter_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is GridSplitter splitter)
        {
            splitter.Background = Brushes.Transparent;
        }
    }
}