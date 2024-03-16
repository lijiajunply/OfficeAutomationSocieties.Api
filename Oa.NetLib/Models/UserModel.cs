namespace Oa.NetLib.Models;

public class UserModel
{
    public string UserId { get; set; } = "";

    public string Name { get; init; } = "";
    public string PhoneNum { get; set; } = "";

    public string Password { get; set; } = "";

    public List<OrganizeModel> Organizes { get; } = [];

    public List<ProjectModel> Projects { get; } = [];

    public List<GanttModel> TaskNotes { get; } = [];
}

[Serializable]
public class LoginModel
{
    public string Password { get; set; } = "";
    public string PhoneNum { get; set; } = "";
}

[Serializable]
public class SignModel
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public string PhoneNum { get; set; } = "";
}