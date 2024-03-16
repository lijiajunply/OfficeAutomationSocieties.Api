using System.Net.Http.Json;
using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class User(string jwt = "") : DataBasic(jwt)
{
    public async Task<string> Login(LoginModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/User/Login", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
        }
    }

    public async Task<string> Signup(SignModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/User/SignUp", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
        }
    }

    public async Task<UserModel> GetUserData()
    {
        try
        {
            var response = await SharedClient.GetAsync("/api/User/GetData");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserModel>(result) ?? new UserModel();
        }
        catch
        {
            return new UserModel();
        }
    }
}