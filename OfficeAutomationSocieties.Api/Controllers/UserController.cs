using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly JwtHelper _jwtHelper;
    private readonly IDbContextFactory<OaContext> _factory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(IDbContextFactory<OaContext> factory, JwtHelper jwtHelper,
        IHttpContextAccessor httpContextAccessor)
    {
        _factory = factory;
        _jwtHelper = jwtHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    #region Visitor

    [TokenActionFilter]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserModel>> GetData()
    {
        await using var _context = await _factory.CreateDbContextAsync();
        var member = _httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        member = await _context.Users.Include(x => x.Projects).FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (member == null) return NotFound();
        member.Password = "";
        return member;
    }

    [HttpPost]
    public async Task<ActionResult<string>> SignUp(LoginModel model)
    {
        await using var _context = await _factory.CreateDbContextAsync();

        if (_context.Users == null!)
        {
            return Problem("Entity set 'MemberContext.Students'  is null.");
        }

        var user = new UserModel(model);
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

        return _jwtHelper.GetMemberToken(user);
    }


    [HttpPost]
    public async Task<ActionResult<string>> Login(LoginModel loginModel)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        if (_context.Users == null!)
            return NotFound();

        var model =
            await _context.Users.FirstOrDefaultAsync(x =>
                x.Name == loginModel.Name && x.Password == loginModel.Password);

        if (model == null)
            return NotFound();
        return _jwtHelper.GetMemberToken(model);
    }

    #endregion

    #region Ordinary Members

    // PUT: api/Member/5
    [TokenActionFilter]
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UserModel memberModel)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        if (id != memberModel.UserId)
        {
            return BadRequest();
        }

        _context.Entry(memberModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Users.AnyAsync(e => e.UserId == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    #endregion
}