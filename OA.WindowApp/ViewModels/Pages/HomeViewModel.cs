using System.Collections.ObjectModel;
using Oa.NetLib.Models;

namespace OA.WindowApp.ViewModels.Pages;

public class HomeViewModel: PageModelBase
{
    public string Name { get; set; } = "";
    public ObservableCollection<ProjectModel> Projects { get; set; } = [];
    public ObservableCollection<OrganizeModel> Organizes { get; set; } = [];
    public ObservableCollection<GanttModel> TaskNotes { get; set; } = [];
}