using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace OfficeAutomationSocieties.Api;

public class JwtHelper(IConfiguration configuration)
{
    public string GetMemberToken(UserJwtModel model)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, model.Identity),
            new Claim(ClaimTypes.PrimarySid, model.UserId),
            new Claim(ClaimTypes.Uri,model.NowOrgId)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now, //notBefore
            expires: DateTime.Now.AddDays(1), //expires
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}

public class TokenActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(bearer) || !bearer.Contains("Bearer")) return;
        var jwt = bearer.Split(' ');
        var tokenObj = new JwtSecurityToken(jwt[1]);

        var claimsIdentity = new ClaimsIdentity(tokenObj.Claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        context.HttpContext.User = claimsPrincipal;
    }
}

public static class TokenHelper
{
    public static UserJwtModel? GetUser(this ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal == null) return null;
        var claimId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
        var claimRole = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var claimNowOrgId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Uri);
        if (claimId.IsNull() || claimRole.IsNull() || claimNowOrgId.IsNull()) return null;

        return new UserJwtModel()
        {
            UserId = claimId!.Value,
            Identity = claimRole!.Value,
            NowOrgId = claimNowOrgId!.Value
        };
    }

    private static bool IsNull(this Claim? claim)
        => claim == null || string.IsNullOrEmpty(claim.Value);
}