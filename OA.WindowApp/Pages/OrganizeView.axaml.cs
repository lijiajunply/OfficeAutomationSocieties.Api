using Avalonia.Controls;
using OA.WindowApp.ViewModels.Pages;

namespace OA.WindowApp.Pages;

public partial class OrganizeView : UserControl
{
    public OrganizeView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        if (DataContext is not OrganizeViewModel model) return;
        if (model.Organizes.Count == 0) return;
        model.Organize = model.Organizes[0];
    }
}