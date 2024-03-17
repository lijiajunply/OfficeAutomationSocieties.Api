using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class GanttModel
{
    [Column(TypeName = "varchar(256)")] public string Id { get; set; } = "";

    public UserModel User { get; } = new();
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; } = "";
    [Column(TypeName = "varchar(256)")]
    public string ProjectId { get; set; } = "";
    public ProjectModel Project { get; } = new();

    [Column(TypeName = "varchar(256)")] public string StartTime { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string EndTime { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string ToDo { get; set; } = "";

    public override string ToString() => $"{User}:{StartTime}-{EndTime}:{ToDo}:{Id}";
}