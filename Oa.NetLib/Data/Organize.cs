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

    public async Task<ProjectModel> CreateOrgProject(ProjectModel model, string id)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync($"/api/Organize/CreateOrgProject/{id}", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProjectModel>(result) ?? new ProjectModel();
        }
        catch
        {
            return new ProjectModel();
        }
    }

    public async Task<bool> QuitOrganize(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/QuitOrganize/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<AnnouncementModel> LookAnnouncement(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/LookAnnouncement/{id}");
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

    public async Task<AnnouncementModel[]> LookAnnouncements(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/LookAnnouncements/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AnnouncementModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> AddAnnouncement(AnnouncementModel model,string id)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync($"/api/Organize/AddAnnouncement/{id}", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveAnnouncement(AnnouncementModel model,string id)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync($"/api/Organize/RemoveAnnouncement/{id}", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ResourceModel[]> GetResources(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/GetResources/{id}");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResourceModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task<bool> UpdateResource(ResourceModel model,string id)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync($"/api/Organize/UpdateResource/{id}", model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ResourceModel> AddResource(ResourceModel model,string id)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync($"/api/Organize/AddResource/{id}", model);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResourceModel>(result) ?? new ResourceModel();
        }
        catch
        {
            return new ResourceModel();
        }
    }

    public async Task<bool> DeleteResource(string id,string org)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/DeleteResource/{id}&{org}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}