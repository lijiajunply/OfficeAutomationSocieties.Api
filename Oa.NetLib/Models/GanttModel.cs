namespace Oa.NetLib.Models;

public class GanttModel
{
    public string Id { get; set; } = "";

    public UserModel User { get; } = new();
    public ProjectModel Project { get; } = new();

    public string StartTime { get; set; } = "";

    public string EndTime { get; set; } = "";

   public string ToDo { get; set; } = "";
}