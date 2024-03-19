namespace Oa.NetLib.Models;

[Serializable]
public class GanttModel
{
    public string Id { get; set; } = "";

    public string UserId { get; set; } = "";
    public string ProjectId { get; set; } = "";
    public string StartTime { get; set; } = "";

    public string EndTime { get; set; } = "";

   public string ToDo { get; set; } = "";
}