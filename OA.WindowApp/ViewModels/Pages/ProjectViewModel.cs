using System.Collections.ObjectModel;
using Oa.NetLib.Models;

namespace OA.WindowApp.ViewModels.Pages;

public class ProjectViewModel : PageModelBase
{
    public ObservableCollection<ProjectModel> Projects { get; set; } = [];
    private ProjectModel _project = new();
    public ProjectModel Project
    {
        get => _project;
        set => SetField(ref _project, value);
    }
}