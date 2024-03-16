using System.Net.Http.Json;
using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class Project(string jwt = "") : DataBasic(jwt)
{
    public async Task<List<ProjectModel>> GetProjects(LoginModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Project/GetProjects", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectModel>>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<ProjectModel> JoinProject(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Project/JoinProject?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectModel>(result) ?? new ProjectModel();
        }
        catch
        {
            return new ProjectModel();
        }
    }

    public async Task<ProjectModel> CreateProject(ProjectModel project)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Project/CreateProject", project);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectModel>(result) ?? new ProjectModel();
        }
        catch
        {
            return new ProjectModel();
        }
    }

    public async Task<bool> RemoveProject(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Project/RemoveProject?id={id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<GanttModel> AddGantt(string id, GanttModel model)
    {
        try
        {
            var response = await SharedClient.PutAsJsonAsync($"/api/Project/AddGantt?id={id}", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GanttModel>(result) ?? new GanttModel();
        }
        catch
        {
            return new GanttModel();
        }
    }

    public async Task<bool> RemoveGantt(string id, string ganttId)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Project/RemoveGantt?id={id}&ganttId={ganttId}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}