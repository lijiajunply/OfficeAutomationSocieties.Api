using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 组织管理系统
/// </summary>
[Authorize]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
public class OrganizeController(
    IDbContextFactory<OaContext> factory,
    IHttpContextAccessor httpContextAccessor,
    JwtHelper jwtHelper)
    : ControllerBase
{
    #region 组织系统

    [HttpGet]
    public async Task<ActionResult<OrganizeModel[]>> GetUserOrganizes()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null || string.IsNullOrEmpty(member.NowOrgId)) return NotFound();

        var user = await _context.Users.Include(x => x.Organizes).ThenInclude(x => x.Organize)
            .FirstOrDefaultAsync(x => x.UserId == member.UserId);

        return user?.Organizes.Select(x => x.Organize).ToArray() ?? [];
    }
    
    [HttpGet]
    public async Task<ActionResult<OrganizeModel>> GetOrgData()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null || string.IsNullOrEmpty(member.NowOrgId)) return NotFound();

        var org = await _context.Organizes.Include(x => x.Projects).Include(x => x.Resources)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);

        if (org == null) return NotFound();
        return org;
    }

    /// <summary>
    /// 创建组织
    /// </summary>
    /// <param name="model"></param>
    /// <returns>新的Jwt</returns>
    [HttpPost]
    public async Task<ActionResult<string>> CreateOrganize([FromBody] OrganizeModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();
        model.Id = $"{model} , Creator is {member.UserId.Base64Encryption()}".HashEncryption();
        var identity = new OrganizeIdentity() { Identity = "President", UserId = user.UserId, Organize = model };
        model.MemberIdentity.Add(identity);
        user.Organizes.Add(identity);
        await _context.SaveChangesAsync();
        var jwt = UserJwtModel.DataToJwt(user);
        jwt.Identity = "President";
        jwt.NowOrgId = model.Id;
        return jwtHelper.GetMemberToken(jwt);
    }

    /// <summary>
    /// 加入组织
    /// </summary>
    /// <param name="id">组织代号</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<string>> AddOrganize(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();

        var org = await _context.Organizes.FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        var identity = new OrganizeIdentity() { User = user, Organize = org };
        org.MemberIdentity.Add(identity);
        user.Organizes.Add(identity);

        await _context.SaveChangesAsync();
        var jwt = UserJwtModel.DataToJwt(user);
        jwt.NowOrgId = id;
        return jwtHelper.GetMemberToken(jwt);
    }

    /// <summary>
    /// 登录到组织
    /// </summary>
    /// <param name="id">组织代号</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<string>> LoginOrganize(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();

        var org = await _context.Organizes.Include(x => x.MemberIdentity).FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        org.MemberIdentity.Add(new OrganizeIdentity() { UserId = user.UserId });
        var identity = org.MemberIdentity.FirstOrDefault(x => x.UserId == member.UserId);
        if (string.IsNullOrEmpty(identity?.Identity)) return NotFound();

        var jwt = UserJwtModel.DataToJwt(user);
        jwt.Identity = identity.Identity;
        jwt.NowOrgId = id;
        return jwtHelper.GetMemberToken(jwt);
    }

    #endregion

    #region 公告系统

    /// <summary>
    /// 查看最新公告
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<AnnouncementModel>> LookAnnouncement()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Announcements)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);
        if (org == null) return NotFound();
        return org.Announcements.LastOrDefault() ?? new AnnouncementModel();
    }

    /// <summary>
    /// 查看所有公告
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<AnnouncementModel>>> LookAnnouncements()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Announcements)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);
        if (org == null) return NotFound();
        return org.Announcements;
    }

    /// <summary>
    /// 添加公告
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "President")]
    [HttpPost]
    public async Task<ActionResult> AddAnnouncement([FromBody] AnnouncementModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Announcements)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);
        if (org == null) return NotFound();

        model.Id = model.ToString().HashEncryption();

        _context.Announcements.Add(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "President")]
    [HttpPost]
    public async Task<ActionResult> RemoveAnnouncement([FromBody] AnnouncementModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        _context.Announcements.Remove(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    #endregion

    #region 资源系统

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceModel>>> GetResources()
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
            return NotFound();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var org = await _context.Organizes.Include(organizeModel => organizeModel.Resources)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);

        return org?.Resources.ToArray() ?? [];
    }

    /// <summary>
    /// 获取资源单例
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceModel>> GetResourceModel(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return NotFound();
        }

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var org = await _context.Organizes.Include(organizeModel => organizeModel.Resources)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);
        var resourceModel = org?.Resources.FirstOrDefault(x => x.Id == id);

        if (resourceModel == null)
        {
            return NotFound();
        }

        return resourceModel;
    }

    /// <summary>
    /// 更新资源状态
    /// </summary>
    /// <param name="resourceModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateResource(ResourceModel resourceModel)
    {
        await using var _context = await factory.CreateDbContextAsync();

        _context.Entry(resourceModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Resources.Any(e => e.Id == resourceModel.Id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }


    /// <summary>
    /// 添加资源
    /// </summary>
    /// <param name="resourceModel"></param>
    /// <returns></returns>
    [Authorize(Roles = "President")]
    [HttpPost]
    public async Task<ActionResult<ResourceModel>> AddResource(ResourceModel resourceModel)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return Problem("Entity set 'OaContext.Resources'  is null.");
        }

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Resources)
            .FirstOrDefaultAsync(x => x.Id == member.NowOrgId);

        resourceModel.Id = resourceModel.ToString().HashEncryption();
        org?.Resources.Add(resourceModel);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (_context.Resources.Any(e => e.Id == resourceModel.Id))
                return Conflict();

            throw;
        }

        return resourceModel;
    }

    /// <summary>
    /// 删除资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "President")]
    public async Task<IActionResult> DeleteResource(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return NotFound();
        }

        var resourceModel = await _context.Resources.FindAsync(id);
        if (resourceModel == null)
        {
            return NotFound();
        }

        _context.Resources.Remove(resourceModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    #endregion
}