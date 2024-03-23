using Avalonia.Controls;
using Avalonia.Interactivity;
using Oa.NetLib.Models;

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

    public (ProjectModel context, bool isCreate) Done()
        => (
            CreateBorder.IsVisible
                ? new ProjectModel() { Name = NameBox.Text ?? "", Introduce = IntroduceBox.Text ?? "" }
                : new ProjectModel() { Id = IdBox.Text ?? "" }, CreateBorder.IsVisible);
}