using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class OrganizeModel
{
    public List<IdentityModel> MemberIdentity { get; } = [];
    [Column(TypeName = "varchar(256)")] public string Name { get; init; } = "";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string Introduce { get; init; } = "";

    public List<AnnouncementModel> Announcements { get; } = [];
    public List<ProjectModel> Projects { get; } = [];
    public List<ResourceModel> Resources { get; } = [];

    public override string ToString()
        => $"Organize : Name is {Name.Base64Encryption()} , Intro is {Introduce.Base64Encryption()} ";
}

public class IdentityModel
{
    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    [Column(TypeName = "varchar(10)")]
    public string Identity { get; init; } = "Member";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; init; } = "";

    public UserModel User { get; set; } = new();

    public OrganizeModel Organize { get; set; } = new();
    [Column(TypeName = "varchar(256)")] public string OrganizeId { get; set; } = "";
}