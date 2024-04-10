namespace Oa.NetLib.Models;

public class ProjectModel
{
    public List<ProjectIdentity> Members { get; } = [];

    public string Name { get; set; } = "";

    public string Id { get; init; } = "";

    public List<FileModel> Files { get; init; } = [];

    public List<GanttModel> GanttList { get; } = [];
    public string Introduce { get; init; } = "";

    public ProjectModel Clone() => (ProjectModel)MemberwiseClone();
}

[Serializable]
public class ProjectIdentity
{
    public string UserId { get; set; } = "";
    public string ProjectId { get; set; } = "";
    public string Identity { get; set; } = "Member";
}