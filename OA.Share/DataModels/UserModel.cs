using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class UserModel
{
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";
    [Column(TypeName = "varchar(13)")] public string PhoneNum { get; init; } = "";

    [Column(TypeName = "varchar(256)")] public string Password { get; set; } = "";

    public List<OrganizeModel> Organizes { get; } = [];

    public List<ProjectModel> Projects { get; } = [];

    public List<GanttModel> TaskNotes { get; } = [];

    public override string ToString() =>
        $"UserModel is {{Name={Name.Base64Encryption()};PhoneNum={PhoneNum.Base64Encryption()};Password={Password.Base64Encryption()}}} Other is Private;";
}

[Serializable]
public class LoginModel
{
    public string Password { get; set; } = "";
    public string PhoneNum { get; set; } = "";
}

[Serializable]
public class SignModel
{
    public string Name { get; set; } = "";
    public string Password { get; set; } = "";
    public string PhoneNum { get; set; } = "";
}