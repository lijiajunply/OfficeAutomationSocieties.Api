using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class ResourceModel
{
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; init; } = "";

    [Column(TypeName = "varchar(256)")]
    public string Name { get; init; } = "";
    public UserModel User { get; init; } = new();
    
    [Column(TypeName = "varchar(256)")]
    public string StartTime { get; init; } = "";
    
    [Column(TypeName = "varchar(256)")]
    public string EndTime { get; init; } = "";

    public override string ToString() =>
        $"ResourceModel is {{Name={Name.Base64Encryption()},StartTime={StartTime.Base64Encryption()},EndTime={EndTime.Base64Encryption()}}} Other is Private;";
}