using System.Net.Http.Headers;

namespace Oa.NetLib.Data;

public abstract class DataBasic : IDisposable
{
    protected DataBasic(string jwt = "")
    {
        // ReSharper disable once RedundantAssignment
        var url = new Uri("https://api.luckyfishes.com");
/*#if DEBUG
        url = new Uri("https://localhost:7060/");
#endif*/
        SharedClient = new HttpClient()
            { BaseAddress = url };
        Jwt = jwt;
    }

    protected HttpClient SharedClient { get; }
    private string _jwt = "";

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