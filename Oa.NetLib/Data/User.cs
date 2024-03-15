using System.Net.Http.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class User : DataBasic
{
    public async Task<string> Login(LoginModel model)
    {
        var response = await SharedClient.PostAsJsonAsync("/api/User/Login", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Signup(LoginModel model)
    {
        var response = await SharedClient.PostAsJsonAsync("/api/User/SignUp", model);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}