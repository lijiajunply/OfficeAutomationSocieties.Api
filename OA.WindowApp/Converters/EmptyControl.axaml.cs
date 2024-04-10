using Avalonia.Controls;
using Avalonia.Interactivity;
using OA.WindowApp.Models;
using OA.WindowApp.Views;

namespace OA.WindowApp.Converters;

public partial class EmptyControl : UserControl
{
    public EmptyControl(string text = "")
    {
        InitializeComponent();
        Block.Text = text;
    }

    private void BaskClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        view?.Navigate("Home");
    }
}