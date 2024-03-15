namespace Oa.NetLib.Models;

public class UserModel
{
    public string UserId { get; set; } = "";

    public string Name { get; init; } = "";

    public string Password { get; set; } = "";

    public List<OrganizeModel> Organizes { get; } = [];

    public List<ProjectModel> Projects { get; } = [];

    public List<GanttModel> TaskNotes { get; } = [];
}

public class LoginModel
{
    public string Password => "";
    public string Name => "";
}