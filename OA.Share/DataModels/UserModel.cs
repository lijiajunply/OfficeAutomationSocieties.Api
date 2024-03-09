using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class UserModel
{
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";

    [Column(TypeName = "varchar(256)")] public string Password { get; set; } = "";

    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    [Column(TypeName = "varchar(256)")]
    public string Identity { get; init; } = "Member";

    public List<ProjectModel> Projects { get; init; } = [];

    public UserModel()
    {
    }

    public UserModel(LoginModel model, string identity = "Member")
    {
        Password = model.Password;
        Name = model.Name;
        UserId = "";
        Identity = identity;
    }

    public override string ToString() => $"UserModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}

public class LoginModel
{
    public string Password { get; set; } = "";
    public string Name { get; set; } = "";
}