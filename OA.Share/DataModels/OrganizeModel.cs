using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class OrganizeModel
{
    public List<UserModel> Member { get; } = [];

    public List<IdentityModel> MemberIdentity { get; } = [];
    [Column(TypeName = "varchar(256)")] public string Name { get; set; } = "";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string Introduce { get; set; } = "";

    public List<AnnouncementModel> Announcements { get; } = [];
    public List<ProjectModel> Projects { get; } = [];
    public List<ResourceModel> Resources { get; } = [];

    public override string ToString()
    {
        return $"Organize : Name is {Name} , Intro is {Introduce} ";
    }
}

public class IdentityModel
{
    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    [Column(TypeName = "varchar(10)")]
    public string Identity { get; set; } = "Member";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string UserId { get; set; } = "";

    public OrganizeModel Owner { get; set; } = new();
}