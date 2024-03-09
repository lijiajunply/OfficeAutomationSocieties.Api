using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class ProjectModel
{
    public List<UserModel> Members { get; init; } = [];

    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; init; } = "";

    public List<FileModel> Files { get; init; } = [];

    /// <summary>
    /// 示例:
    /// User1 : 2023/01/01 - 2023/02/01 : 写代码 : E!@# (计划代号)
    /// User2 : 2023/03/01 - 2023/04/01 : 写代码 : E!@# (计划代号)
    /// </summary>
    public List<GanttModel> GanttsList { get; set; } = [];

    public override string ToString() => $"ProjectModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}

public class GanttModel
{
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; }= "";
    
    [Column(TypeName = "varchar(256)")]
    public string User { get; set; }= "";
    
    [Column(TypeName = "varchar(256)")]
    public string StartTime { get; set; }= "";
    
    [Column(TypeName = "varchar(256)")]
    public string EndTime { get; set; }= "";
    
    [Column(TypeName = "varchar(256)")]
    public string ToDo { get; set; }= "";

    public override string ToString() => $"{User}:{StartTime}-{EndTime}:{ToDo}:{Id}";
}