using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class ResourceModel
{ 
    /// <summary>
    /// 所有者
    /// </summary>
    public OrganizeModel Owner { get; set; } = new();

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; init; } = "";

    /// <summary>
    /// 资源名称
    /// </summary>
    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";

    
    /// <summary>
    /// 借出时间，如果为空则为未借出
    /// </summary>
    [Column(TypeName = "varchar(256)")] public string StartTime { get; init; } = "";

    /// <summary>
    /// 归还时间，不能为空
    /// </summary>
    [Column(TypeName = "varchar(256)")] public string EndTime { get; init; } = "";

    public override string ToString() =>
        $"ResourceModel is {{Name={Name.Base64Encryption()},StartTime={StartTime.Base64Encryption()},EndTime={EndTime.Base64Encryption()}}} Other is Private;";
}