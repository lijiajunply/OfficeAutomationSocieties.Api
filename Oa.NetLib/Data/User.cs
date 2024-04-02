using System.Net.Http.Json;
using Newtonsoft.Json;
using Oa.NetLib.Models;

namespace Oa.NetLib.Data;

public class User(string jwt = "") : DataBasic(jwt)
{
    
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> Login(LoginModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/User/Login", model);
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 更新用户数据
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> Update(UserModel model)
    {
        try
        {
            var response = await SharedClient.PostAsJsonAsync("/api/User/Update",model);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}