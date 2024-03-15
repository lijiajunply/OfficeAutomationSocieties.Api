namespace Oa.NetLib.Models;

public class FileModel : IFile
{
    public string Path { get; set; } = "";
    
    public string Url { get; set; } = "";

    public bool IsFolder { get; set; }

    public ProjectModel? Owner { get; init; }
    
    public string Id { get; set; } = "";

    public FolderModel ToFolder() => new() { IsFolder = IsFolder, Path = Path, Url = Url, Id = Id };
}

public class FolderModel : IFile
{
    public string Path { get; set; } = "";
    public string Url { get; set; }= "";
    public string Id { get; set; }= "";

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