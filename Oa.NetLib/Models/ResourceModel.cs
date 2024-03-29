using Newtonsoft.Json;

namespace Oa.NetLib.Models;

[Serializable]
public class ResourceModel
{
    /// <summary>
    /// 所有者
    /// </summary>
    [JsonIgnore]
    public OrganizeModel Owner { get; init; } = new();

    public string Id { get; set; } = "";

    /// <summary>
    /// 资源名称
    /// </summary>
    public string Name { get; init; } = "";

    public string Introduce { get; set; } = "";
    public string CreateTime { get; init; } = "";

    /// <summary>
    /// 借出时间，如果为空则为未借出
    /// </summary>
    public string? StartTime { get; init; }

    /// <summary>
    /// 归还时间，不能为空
    /// </summary>
    public string? EndTime { get; init; }
}