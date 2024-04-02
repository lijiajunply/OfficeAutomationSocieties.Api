namespace Oa.NetLib.Models;

public class OrganizeModel
{
    public List<OrganizeIdentity> MemberIdentity { get; } = [];
    public string Name { get; init; } = "";

    public string Id { get; init; } = "";

    public string Introduce { get; init; } = "";
    public List<ProjectModel> Projects { get; } = [];
}

[Serializable]
public class OrganizeIdentity
{
    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    public string Identity { get; init; } = "Member";

    public string UserId { get; init; } = "";
}