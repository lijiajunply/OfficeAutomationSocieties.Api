using Avalonia.Controls;
using Avalonia.Interactivity;
using OA.WindowApp.Models;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class SettingView : UserControl
{
    public SettingView()
    {
        InitializeComponent();
    }

    private void LogoutClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        view?.Logout();
    }
}