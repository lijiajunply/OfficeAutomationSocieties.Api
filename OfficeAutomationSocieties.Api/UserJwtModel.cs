using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api;

public class UserJwtModel
{
    public string UserId { get; init; } = "";

    public string NowOrgId { get; set; } = "Public";

    /// <summary>
    /// President
    /// Minister
    /// Member
    /// </summary>
    public string Identity { get; set; } = "Member";

    public static UserJwtModel DataToJwt(UserModel model)
    {
        return new UserJwtModel()
        {
            UserId = model.UserId
        };
    }
}