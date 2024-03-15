namespace Oa.NetLib.Models;

/// <summary>
/// 公告
/// </summary>
public class AnnouncementModel
{
    public string Context { get; init; } = "";

    public OrganizeModel Owner { get; init; } = new();

    public string Time { get; init; } = "";

    public string Id { get; set; } = "";
}