using Avalonia.Controls;
using Avalonia.Interactivity;
using Oa.NetLib.Models;

namespace OA.WindowApp.Dialogs;

public partial class JoinOrCreateOrganize : UserControl
{
    public JoinOrCreateOrganize()
    {
        InitializeComponent();
    }

    private void ConvertClick(object? sender, RoutedEventArgs e)
    {
        CreateBorder.IsVisible = !CreateBorder.IsVisible;
    }

    public (OrganizeModel organize, bool isCreate) Done()
        => ( CreateBorder.IsVisible
                ? new OrganizeModel() { Name = NameBox.Text ?? "", Introduce = IntroduceBox.Text ?? "" }
                : new OrganizeModel() { Id = IdBox.Text ?? "" },
            CreateBorder.IsVisible);
}