using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 用户系统
/// </summary>
/// <param name="factory"></param>
/// <param name="jwtHelper"></param>
/// <param name="httpContextAccessor"></param>
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController(
    IDbContextFactory<OaContext> factory,
    JwtHelper jwtHelper,
    IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    #region Visitor

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserModel>> GetData()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var user = await _context.Users.Include(x => x.Projects)
            .Include(x => x.Organizes)
            .Include(x => x.TaskNotes)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        user.Password = "";
        return user;
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<string>> SignUp(SignModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();

        if (_context.Users == null!)
        {
            return Problem("Entity set 'MemberContext.Students'  is null.");
        }

        var user = new UserModel()
        {
            Password = model.Password,
            PhoneNum = model.PhoneNum,
            Name = model.Name
        };
        user.UserId = user.ToString().HashEncryption();

        _context.Users.Add(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict();
        }

        return jwtHelper.GetMemberToken(UserJwtModel.DataToJwt(user));
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Users == null!)
            return NotFound();

        var model =
            await _context.Users.FirstOrDefaultAsync(x =>
                x.PhoneNum == loginModel.PhoneNum && x.Password == loginModel.Password);

        if (model == null)
            return NotFound();
        return jwtHelper.GetMemberToken(UserJwtModel.DataToJwt(model));
    }

    #endregion

    #region Ordinary Members

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="memberModel"></param>
    /// <returns></returns>
    [TokenActionFilter]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Update(UserModel memberModel)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member?.UserId != memberModel.UserId) return BadRequest();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        user.Update(memberModel);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            return BadRequest();
        }

        return NoContent();
    }

    #endregion
}