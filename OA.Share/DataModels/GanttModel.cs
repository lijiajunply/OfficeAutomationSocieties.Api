using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// ReSharper disable MemberCanBePrivate.Global

namespace OA.Share.DataModels;

public class GanttModel
{
    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    [JsonIgnore] public UserModel User { get; set; } = new();
    [Column(TypeName = "varchar(64)")] public string UserId { get; init; } = "";
    [Column(TypeName = "varchar(64)")] public string ProjectId { get; init; } = "";
    [JsonIgnore] public ProjectModel Project { get; } = new();

    [Column(TypeName = "boolean")]
    public bool IsDone { get; set; }
    [Column(TypeName = "varchar(64)")] public string StartTime { get; set; } = "";

    [Column(TypeName = "varchar(64)")] public string EndTime { get; set; } = "";

    [Column(TypeName = "varchar(64)")] public string ToDo { get; set; } = "";

    public override string ToString() => $"{User}:{StartTime}-{EndTime}:{ToDo}";
    
    public void Update(GanttModel model)
    {
        if (!string.IsNullOrEmpty(model.StartTime)) StartTime = model.StartTime;
        if (!string.IsNullOrEmpty(model.EndTime)) EndTime = model.EndTime;
        if (!string.IsNullOrEmpty(model.ToDo)) ToDo = model.ToDo;
        IsDone = model.IsDone;
    }
}