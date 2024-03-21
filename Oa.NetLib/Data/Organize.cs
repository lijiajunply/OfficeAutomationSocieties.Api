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
            var response = await SharedClient.GetAsync("/api/Organize/GetOrgData");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrganizeModel[]>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }
    
    public async Task<OrganizeModel> GetOrgData()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Organize/GetOrgData");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrganizeModel>(result) ?? new OrganizeModel();
        }
        catch
        {
            return new OrganizeModel();
        }
    }

    public async Task<string> CreateOrganize(OrganizeModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/Organize/CreateOrganize", model);
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
        }
    }

    public async Task<string> AddOrganize(string id)
    {
        try
        {
            var response = await SharedClient.GetAsync($"/api/Organize/AddOrganize/{id}");
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
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
}