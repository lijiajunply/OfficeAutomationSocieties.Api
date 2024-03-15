﻿namespace Oa.NetLib.Models;

public class ProjectModel
{
    public List<UserModel> Members { get; } = [];

    public string Name { get; set; } = "";

    public string Id { get; set; } = "";

    public List<FileModel> Files { get; init; } = [];

    public List<GanttModel> GanttList { get; } = [];
}