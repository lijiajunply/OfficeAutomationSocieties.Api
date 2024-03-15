using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA.Share.DataModels;

public class OrganizeModel
{
    public List<UserModel> Member { get; set; } = [];
    [Column(TypeName = "varchar(256)")] public string Name { get; set; } = "";

    [Key]
    [Column(TypeName = "varchar(256)")]
    public string Id { get; set; } = "";

    [Column(TypeName = "varchar(256)")] public string Introduce { get; set; } = "";

    public List<AnnouncementModel> Announcements { get; set; } = [];
    public List<ProjectModel> Projects { get; set; } = [];
    public List<ResourceModel> Resources { get; set; } = [];
}