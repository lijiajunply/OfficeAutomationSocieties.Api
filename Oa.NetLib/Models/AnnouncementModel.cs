namespace Oa.NetLib.Models;

/// <summary>
/// 公告
/// </summary>
[Serializable]
public class AnnouncementModel
{
    public string Context { get; init; } = "";
    public string Title { get; init; } = "";

    public string Time { get; init; } = "";
    public string Id { get; set; } = "";
}