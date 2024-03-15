using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Oa.NetLib.Data;

public abstract class DataBasic : IDisposable
{
    protected DataBasic(string jwt = "")
    {
        SharedClient = new HttpClient()
            { BaseAddress = new Uri("https://api.luckyfishes.com/api") };
        Jwt = jwt;
    }

    private HttpClient SharedClient { get; }
    private string _jwt = "";
    public static string SwaggerUrl = "https://api.luckyfishes.com/swagger/index.html";

    public string Jwt
    {
        get => _jwt;
        set
        {
            _jwt = value;
            SharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwt);
        }
    }

    protected async Task<JObject?> GetFormString(string url)
    {
        try
        {
            return JObject.Parse(await SharedClient.GetStringAsync(url));
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e.Message);
#endif
            return null;
        }
    }

    public void Dispose()
    {
        SharedClient.Dispose();
        GC.SuppressFinalize(this);
    }
}