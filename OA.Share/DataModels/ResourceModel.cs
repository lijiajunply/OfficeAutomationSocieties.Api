using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OA.Share.DataModels;

public class ResourceModel
{
    /// <summary>
    /// 所有者
    /// </summary>
    [JsonIgnore]
    public OrganizeModel Owner { get; init; } = new();

    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    /// <summary>
    /// 资源名称
    /// </summary>
    [Column(TypeName = "varchar(32)")]
    public string Name { get; init; } = "";

    [Column(TypeName = "varchar(50)")] public string Introduce { get; init; } = "";

    [Column(TypeName = "varchar(64)")] public string CreateTime { get; init; } = "";

    /// <summary>
    /// 借出时间，如果为空则为未借出
    /// </summary>
    [Column(TypeName = "varchar(64)")]
    public string? StartTime { get; init; }

    /// <summary>
    /// 归还时间，不能为空
    /// </summary>
    [Column(TypeName = "varchar(64)")]
    public string? EndTime { get; init; }

    public override string ToString() =>
        $"ResourceModel is {{Name={Name.Base64Encryption()}Introduce={Introduce.Base64Encryption()},StartTime={StartTime?.Base64Encryption()},EndTime={EndTime?.Base64Encryption()}}} Other is Private;";
}