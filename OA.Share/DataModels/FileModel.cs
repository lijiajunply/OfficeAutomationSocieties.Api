using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class FileModel
{
    /// <summary>
    /// 现实路径
    /// </summary>
    [Column(TypeName = "varchar(256)")]
    public string Path { get; set; } = "";

    /// <summary>
    /// 网盘虚拟路径
    /// </summary>
    [Column(TypeName = "varchar(256)")]
    public string Url { get; set; } = "";

    public bool IsFolder { get; set; }

    public ProjectModel? Owner { get; init; }

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; } = "";
    
    public override string ToString() =>
        $"FileModel is {{Path={Path.Base64Encryption()},Url={Url.Base64Encryption()},IsFolder={new Random().Next(IsFolder.ToString().Length)}}} Other is Private;";
}