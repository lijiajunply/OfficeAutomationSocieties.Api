using System.Collections.ObjectModel;
using Oa.NetLib.Models;

namespace OA.WindowApp.ViewModels.Pages;

public class OrganizeViewModel : PageModelBase
{
    public ObservableCollection<OrganizeModel> Organizes { get; set; } = [];
    private OrganizeModel _organize = new();

    public OrganizeModel Organize
    {
        get => _organize;
        set => SetField(ref _organize, value);
    }
}