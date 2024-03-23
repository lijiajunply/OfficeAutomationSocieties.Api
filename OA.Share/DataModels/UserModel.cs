using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class UserModel
{
    [Key]
    [Column(TypeName = "varchar(64)")]
    public string UserId { get; set; } = "";

    [Column(TypeName = "varchar(32)")]
    public string RegistrationTime { get; set; } = "";
    [Column(TypeName = "varchar(32)")] public string Name { get; set; } = "";
    [Column(TypeName = "varchar(13)")] public string PhoneNum { get; set; } = "";

    [Column(TypeName = "varchar(16)")] public string Password { get; set; } = "";

    public List<OrganizeIdentity> Organizes { get; } = [];

    public List<ProjectIdentity> Projects { get; } = [];

    public List<GanttModel> TaskNotes { get; } = [];

    public void Update(UserModel model)
    {
        if (!string.IsNullOrEmpty(model.Name)) Name = model.Name;
        if (!string.IsNullOrEmpty(model.PhoneNum)) PhoneNum = model.PhoneNum;
        if (!string.IsNullOrEmpty(model.Password)) Password = model.Password;
        if (!string.IsNullOrEmpty(model.RegistrationTime)) RegistrationTime = model.RegistrationTime;
    }

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