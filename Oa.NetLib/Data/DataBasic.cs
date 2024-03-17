using System.Net.Http.Headers;

namespace Oa.NetLib.Data;

public abstract class DataBasic : IDisposable
{
    protected DataBasic(string jwt = "")
    {
        SharedClient = new HttpClient()
            { BaseAddress = new Uri("https://localhost:7060/") };
        Jwt = jwt;
    }

    protected HttpClient SharedClient { get; }
    private string _jwt = "";
    // public static string SwaggerUrl => "https://api.luckyfishes.com/swagger/index.html";

    public string Jwt
    {
        get => _jwt;
        set
        {
            _jwt = value;
            if (string.IsNullOrEmpty(_jwt)) return;
            SharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Jwt);
        }
    }

    public void Dispose()
    {
        SharedClient.Dispose();
        GC.SuppressFinalize(this);
    }
}