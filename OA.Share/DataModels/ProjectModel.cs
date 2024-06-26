﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OA.Share.DataModels;

public class ProjectModel
{
    public List<ProjectIdentity> Members { get;} = [];

    [Column(TypeName = "varchar(32)")] public string Name { get; set; } = "";

    [Column(TypeName = "varchar(512)")] public string Introduce { get; set; } = "";

    [Key]
    [Column(TypeName = "varchar(64)")]
    public string Id { get; set; } = "";

    public List<FileModel> Files { get; init; } = [];

    public List<GanttModel> GanttList { get; } = [];

    public void Update(ProjectModel model)
    {
        if (!string.IsNullOrEmpty(model.Name)) Name = model.Name;
        if (!string.IsNullOrEmpty(model.Introduce)) Introduce = model.Introduce;
    }

    public override string ToString() => $"ProjectModel is {{Name={Name.Base64Encryption()}}} Other is Private;";
}

public class ProjectIdentity
{
    [JsonIgnore]
    [Key]
    [Column(TypeName = "varchar(150)")]
    public string Key { get; init; } = "";

    /// <summary>
    /// Minister
    /// Member
    /// </summary>
    [Column(TypeName = "varchar(10)")]
    public string Identity { get; init; } = "Member";

    [Column(TypeName = "varchar(64)")] public string UserId { get; init; } = "";

    [Column(TypeName = "varchar(64)")] public string ProjectId { get; init; } = "";
    [JsonIgnore] public UserModel User { get; init; } = new();

    [JsonIgnore] public ProjectModel Project { get; init; } = new();
}