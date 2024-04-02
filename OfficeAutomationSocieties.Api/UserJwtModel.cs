using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api;

public class UserJwtModel
{
    public string UserId { get; init; } = "";

    public static UserJwtModel DataToJwt(UserModel model)
    {
        return new UserJwtModel()
        {
            UserId = model.UserId
        };
    }
}