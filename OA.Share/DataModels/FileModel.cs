using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class FileModel : IFile
{
    /// <summary>
    /// 现实路径
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// 网盘虚拟路径
    /// </summary>
    public string Url { get; set; } = "";
    public bool IsFolder { get; set; }

    public ProjectModel? Owner { get; set; }

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; }

    public FolderModel ToFolder() => new() { IsFolder = IsFolder, Path = Path, Url = Url, Id = Id };
    
    public override string ToString() => $"FileModel is {{Path={Path.Base64Encryption()},Url={Url.Base64Encryption()},IsFolder={new Random().Next(IsFolder.ToString().Length)}}} Other is Private;";
}

public class FolderModel : IFile
{
    public string Path { get; set; }
    public string Url { get; set; }
    public string Id { get; set; }

    public bool IsFolder { get; set; }
}

public class FileInfoModel
{
    public string Size { get; set; }

    public FileInfoModel(IFile model)
    {
        var info = new FileInfo(model.GetUrl());
        Size = info.FileSize().FileSizeString();
    }
}

public interface IFile
{
    public string Path { get; set; }
    public string Url { get; set; }
    public string Id { get; set; }
    public bool IsFolder { get; set; }
}