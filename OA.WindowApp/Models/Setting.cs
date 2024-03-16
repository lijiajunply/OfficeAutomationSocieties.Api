using System.IO;
using Oa.NetLib.Models;

namespace OA.WindowApp.Models;

public static class SettingStatic
{
    public static void Save(this LoginModel model) =>
        File.WriteAllText("cookie.json", System.Text.Json.JsonSerializer.Serialize(model));

    public static bool IsNull(this LoginModel model) =>
        string.IsNullOrEmpty(model.PhoneNum) || string.IsNullOrEmpty(model.Password);

    public static LoginModel Read()
    {
        if (!File.Exists("cookie.json"))
        {
            var model = new LoginModel();
            model.Save();
            return model;
        }

        var jsonContext = File.ReadAllText("cookie.json");
        return System.Text.Json.JsonSerializer.Deserialize<LoginModel>(jsonContext) ?? new LoginModel();
    }
}