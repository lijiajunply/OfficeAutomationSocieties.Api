using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class ProjectModel
{
    public List<UserModel> Members { get; set; } = new();
    public string Name { get; set; }
    
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; }
    
    public List<FileModel> Files { get; set; }
    
    /// <summary>
    /// 示例:
    /// User1 : 2023/01/01 - 2023/02/01 : 写代码 : E!@# (计划代号)
    /// User2 : 2023/03/01 - 2023/04/01 : 写代码 : E!@# (计划代号)
    /// </summary>
    public string Gantt { get; set; }
    
    public override string ToString() => $"ProjectModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}

public class GanttModel
{
    public string Id { get; set; }
    public string User { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string ToDo { get; set; }

    public GanttModel() { }

    public GanttModel(string s)
    {
        var gantt = s.Split(':');
        if(gantt.Length != 3)return;
        User = gantt[0];
        ToDo = gantt[2];
        Id = gantt[^1];
        var time = gantt[1];
        var times = time.Split('-');
        if(times.Length != 2)return;
        StartTime = times[0];
        EndTime = times[0];
    }

    public override string ToString() => $"{User}:{StartTime}-{EndTime}:{ToDo}:{Id}";
}