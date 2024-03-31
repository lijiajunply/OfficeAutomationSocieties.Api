using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OA.Share.DataModels;

/// <summary>
/// 公告
/// </summary>
public class AnnouncementModel
{
    /// <summary>
    /// 公告内容，使用Markdown
    /// </summary>
    [Column(TypeName = "varchar(500)")]
    public string Context { get; init; } = "";

    /// <summary>
    /// 公告标题
    /// </summary>
    [Column(TypeName = "varchar(25)")]
    public string Title { get; init; } = "";
    
    [JsonIgnore]
    public OrganizeModel Owner { get; init; } = new();

    [Column(TypeName = "varchar(256)")] public string Time { get; init; } = "";

    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    public override string ToString() =>
        $"AnnouncementModel is {{Context={Context.Base64Encryption()},Time={Time.Base64Encryption()}}} Other is Private;";
}