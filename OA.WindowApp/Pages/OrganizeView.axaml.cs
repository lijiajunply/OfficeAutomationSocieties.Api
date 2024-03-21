using System.Linq;
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
        if (model.Organizes.Count == 0)
        {
            Viewer.IsVisible = false;
            return;
        }

        model.Organize = model.Organizes[0];
    }

    public void OrganizeViewFromId(string id)
    {
        if (DataContext is not OrganizeViewModel model) return;
        var p = model.Organizes.FirstOrDefault(x => x.Id == id);
        if (p != null) model.Organize = p;
    }
}