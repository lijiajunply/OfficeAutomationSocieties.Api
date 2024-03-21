using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OA.Share.DataModels;

public class ProjectModel
{
    public List<ProjectIdentity> Members { get; } = [];

    [Column(TypeName = "varchar(32)")] public string Name { get; init; } = "";

    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    public List<FileModel> Files { get; init; } = [];

    public List<GanttModel> GanttList { get; } = [];

    public override string ToString() => $"ProjectModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}

public class ProjectIdentity
{
    [JsonIgnore] [Key] public int Key { get; set; }
    
    [Column(TypeName = "varchar(64)")] public string UserId { get; set; } = "";

    [Column(TypeName = "varchar(64)")] public string ProjectId { get; set; } = "";
    [JsonIgnore] public UserModel User { get; init; } = new();

    [JsonIgnore] public ProjectModel Project { get; } = new();
    //[Column(TypeName = "varchar(10)")] public string ProjIdentity { get; set; } = "";
}