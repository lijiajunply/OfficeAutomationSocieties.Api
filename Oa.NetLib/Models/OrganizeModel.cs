namespace Oa.NetLib.Models;

public class OrganizeModel
{
    public List<UserModel> Member { get; } = [];

    public List<IdentityModel> MemberIdentity { get; } = [];
    public string Name { get; init; } = "";

    public string Id { get; set; } = "";

    public string Introduce { get; init; } = "";

    public List<AnnouncementModel> Announcements { get; } = [];
    public List<ProjectModel> Projects { get; } = [];
    public List<ResourceModel> Resources { get; } = [];
}

[Serializable]
public class IdentityModel
{
    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    public string Identity { get; init; } = "Member";

    public string UserId { get; init; } = "";

    public OrganizeModel Owner { get; } = new();
}