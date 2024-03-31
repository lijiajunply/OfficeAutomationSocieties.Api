namespace Oa.NetLib.Models;

[Serializable]
public class ResourceModel
{
    public string Id { get; set; } = "";

    /// <summary>
    /// 资源名称
    /// </summary>
    public string Name { get; set; } = "";

    public string Introduce { get; set; } = "";
    public string CreateTime { get; set; } = "";

    /// <summary>
    /// 借出时间，如果为空则为未借出
    /// </summary>
    public string? StartTime { get; set; }

    /// <summary>
    /// 归还时间，不能为空
    /// </summary>
    public string? EndTime { get; set; }
}