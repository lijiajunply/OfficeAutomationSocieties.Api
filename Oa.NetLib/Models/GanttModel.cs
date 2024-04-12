using System.Text.Json.Serialization;

namespace Oa.NetLib.Models;

[Serializable]
public class GanttModel
{
    public string Id { get; set; } = "";

    public string UserId { get; set; } = "";
    public string ProjectId { get; set; } = "";
    [JsonIgnore]
    public bool IsExpired
    {
        get
        {
            if(!DateTime.TryParse(EndTime,out var date))return false;
            return date < DateTime.Today && !IsDone;
        }
    }

    public bool IsDone { get; set; }
    public string StartTime { get; set; } = "";

    public string EndTime { get; set; } = "";

   public string ToDo { get; set; } = "";
   
   public void Update(GanttModel model)
   {
       if (!string.IsNullOrEmpty(model.StartTime)) StartTime = model.StartTime;
       if (!string.IsNullOrEmpty(model.EndTime)) EndTime = model.EndTime;
       if (!string.IsNullOrEmpty(model.ToDo)) ToDo = model.ToDo;
   }
}