using System.Collections.ObjectModel;
using Oa.NetLib.Models;

namespace OA.WindowApp.ViewModels.Pages;

public class HomeViewModel: PageModelBase
{
    private UserModel _user = new();
    public UserModel User
    {
        get => _user;
        set => SetField(ref _user, value);
    }
    public ObservableCollection<ProjectModel> Projects { get; set; } = [];
    public ObservableCollection<OrganizeModel> Organizes { get; set; } = [];
    public ObservableCollection<GanttModel> TaskNotes { get; set; } = [];
}