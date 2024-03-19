using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class Organize(string jwt = "") : DataBasic(jwt)
{
    public async Task<List<ProjectModel>> GetProjects()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/Project/GetProjects");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectModel>>(result) ?? [];
        }
        catch
        {
            return [];
        }
    }
}