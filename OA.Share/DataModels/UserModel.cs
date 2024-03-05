using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class UserModel
{
    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; }

    public string Name { get; set; }
    public string Password { get; set; }

    /// <summary>
    /// President : 社长,副社长,秘书长
    /// Minister : 部长
    /// Member : 普通成员
    /// </summary>
    public string Identity { get; set; } = "Member";

    public List<ProjectModel> Projects { get; set; } = new();

    public UserModel() { }

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
    public string Password { get; set; }
    public string Name { get; set; }
}