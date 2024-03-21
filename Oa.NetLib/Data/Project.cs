using System.Net.Http.Json;
using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class Project(string jwt = "") : DataBasic(jwt)
{
    public async Task<ProjectModel[]> GetUserProjects()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Project/GetUserProjects");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectModel[]>(result) ?? [];
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
            var response = await SharedClient.GetAsync($"/api/Project/JoinProject/{id}");
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
            var response = await SharedClient.GetAsync($"/api/Project/RemoveProject/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<GanttModel> AddGantt(string projectId, GanttModel model)
    {
        try
        {
            var response = await SharedClient.PutAsJsonAsync($"/api/Project/AddGantt/{projectId}", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GanttModel>(result) ?? new GanttModel();
        }
        catch
        {
            return new GanttModel();
        }
    }

    public async Task<bool> UpdateGantt(GanttModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Project/UpdateGantt", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveGantt(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Project/RemoveGantt/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}