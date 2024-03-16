using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class ProjectModel
{
    public List<UserModel> Members { get; } = [];

    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; } = "";

    public List<FileModel> Files { get; init; } = [];

    public List<GanttModel> GanttList { get; } = [];

    public override string ToString() => $"ProjectModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}