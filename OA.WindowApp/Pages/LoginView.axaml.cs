using Avalonia.Controls;
using Avalonia.Interactivity;
using Oa.NetLib.Data;
using Oa.NetLib.Models;
using OA.WindowApp.Models;
using OA.WindowApp.Views;

namespace OA.WindowApp.Pages;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private async void PhoneLoginClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        await view.Login(new LoginModel() { PhoneNum = PhoneBox.Text ?? "", Password = PasswordBox.Text ?? "" });
    }

    private void ConvertClick(object? sender, RoutedEventArgs e)
    {
        SignBorder.IsVisible = !SignBorder.IsVisible;
    }

    private async void SignClick(object? sender, RoutedEventArgs e)
    {
        var view = ViewOpera.GetView<MainWindow>(this);
        if (view == null) return;
        await view.Signup(new SignModel()
        {
            PhoneNum = PhoneSignBox.Text ?? "", Password = PasswordSignBox.Text ?? "", Name = NameSignBox.Text ?? ""
        });
    }
}