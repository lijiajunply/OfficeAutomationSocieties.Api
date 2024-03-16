using Avalonia.Controls;
using Avalonia.Interactivity;

namespace OA.WindowApp.Dialogs;

public partial class JoinOrCreateProject : UserControl
{
    public JoinOrCreateProject()
    {
        InitializeComponent();
    }

    private void ConvertClick(object? sender, RoutedEventArgs e)
    {
        CreateBorder.IsVisible = !CreateBorder.IsVisible;
    }

    public (string context, bool isCreate) Done()
        => ((CreateBorder.IsVisible ? NameBox.Text : IdBox.Text) ?? "", CreateBorder.IsVisible);
}