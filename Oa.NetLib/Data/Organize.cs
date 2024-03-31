using System.Net.Http.Json;
using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class Organize(string jwt = "") : DataBasic(jwt)
{
    public async Task<OrganizeModel[]> GetUserOrganizes()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Organize/GetUserOrganizes");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrganizeModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<OrganizeModel> CreateOrganize(OrganizeModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/CreateOrganize", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrganizeModel>(result) ?? new OrganizeModel();
        }
        catch
        {
            return new OrganizeModel();
        }
    }

    public async Task<OrganizeModel> AddOrganize(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/AddOrganize/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrganizeModel>(result) ?? new OrganizeModel();
        }
        catch
        {
            return new OrganizeModel();
        }
    }

    public async Task<string> LoginOrganize(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/LoginOrganize/{id}");
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
        }
    }

    public async Task<ProjectModel> CreateOrgProject(ProjectModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/CreateOrgProject", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectModel>(result) ?? new ProjectModel();
        }
        catch
        {
            return new ProjectModel();
        }
    }

    public async Task<AnnouncementModel> LookAnnouncement()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Organize/LookAnnouncement");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AnnouncementModel>(result) ?? new AnnouncementModel();
        }
        catch
        {
            return new AnnouncementModel();
        }
    }

    public async Task<UserModel[]> GetOrganizeMember(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/GetOrganizeMember/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }
    
    public async Task<AnnouncementModel[]> LookAnnouncements()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Organize/LookAnnouncements");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AnnouncementModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> AddAnnouncement(AnnouncementModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/AddAnnouncement", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveAnnouncement(AnnouncementModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/RemoveAnnouncement", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ResourceModel[]> GetResources()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Organize/GetResources");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResourceModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> UpdateResource(ResourceModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/UpdateResource", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ResourceModel> AddResource(ResourceModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/AddResource", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResourceModel>(result) ?? new ResourceModel();
        }
        catch
        {
            return new ResourceModel();
        }
    }

    public async Task<bool> DeleteResource(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/DeleteResource/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}