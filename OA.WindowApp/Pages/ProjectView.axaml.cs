using System.Linq;
using Avalonia.Controls;
using OA.WindowApp.ViewModels.Pages;

namespace OA.WindowApp.Pages;

public partial class ProjectView : UserControl
{
    public ProjectView()
    {
        InitializeComponent();
    }

    protected override void OnInitialized()
    {
        if (DataContext is not ProjectViewModel model) return;
        if(model.Projects.Count == 0)return;
        model.Project = model.Projects[0];
    }

    public void ProjectViewFromId(string id)
    {
        if (DataContext is not ProjectViewModel model) return;
        var p = model.Projects.FirstOrDefault(x => x.Id == id);
        if (p != null) model.Project = p;
    }
}