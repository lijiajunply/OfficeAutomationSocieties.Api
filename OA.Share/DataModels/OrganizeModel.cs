using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OA.Share.DataModels;

public class OrganizeModel
{
    public List<OrganizeIdentity> MemberIdentity { get; } = [];
    [Column(TypeName = "varchar(32)")] public string Name { get; set; } = "";

    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    [Column(TypeName = "varchar(512)")] public string Introduce { get; set; } = "";

    public List<AnnouncementModel> Announcements { get; } = [];
    public List<ProjectModel> Projects { get; } = [];
    public List<ResourceModel> Resources { get; } = [];

    public void Update(OrganizeModel model)
    {
        if (!string.IsNullOrEmpty(model.Name)) Name = model.Name;
        if (!string.IsNullOrEmpty(model.Introduce)) Introduce = model.Introduce;
    }

    public override string ToString()
        => $"Organize : Name is {Name.Base64Encryption()} , Intro is {Introduce.Base64Encryption()} ";
}

public class OrganizeIdentity
{
    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    [Column(TypeName = "varchar(10)")]
    public string Identity { get; init; } = "Member";

    [Key]
    [JsonIgnore]
    [Column(TypeName = "varchar(150)")]
    public string Key { get; init; } = "";

    [Column(TypeName = "varchar(64)")] public string UserId { get; init; } = "";

    [JsonIgnore] public UserModel User { get; init; } = new();
    [JsonIgnore] public OrganizeModel Organize { get; init; } = new();
    [Column(TypeName = "varchar(64)")] public string OrganizeId { get; init; } = "";
}