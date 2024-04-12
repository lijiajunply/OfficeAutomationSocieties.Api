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
    IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    #region 组织系统

    [HttpGet]
    public async Task<ActionResult<OrganizeModel[]>> GetUserOrganizes()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var user = await _context.Users.Include(x => x.Organizes)
            .ThenInclude(x => x.Organize)
            .ThenInclude(x => x.Projects)
            .FirstOrDefaultAsync(x => x.UserId == member.UserId);

        return user?.Organizes.Select(x => x.Organize).ToArray() ?? [];
    }

    /// <summary>
    /// 创建组织
    /// </summary>
    /// <param name="model"></param>
    /// <returns>新的Jwt</returns>
    [HttpPost]
    public async Task<ActionResult<OrganizeModel>> CreateOrganize([FromBody] OrganizeModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();
        model.Id = model.ToString().HashEncryption();

        model.MemberIdentity.Add(new OrganizeIdentity()
            { Identity = "President", User = user, Key = $"U-{user.UserId}|O-{model.Id}" });

        await _context.Organizes.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    /// <summary>
    /// 加入组织
    /// </summary>
    /// <param name="id">组织代号</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizeModel>> AddOrganize(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();

        var org = await _context.Organizes.FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        var identity = new OrganizeIdentity()
            { User = user, Organize = org, Key = $"U-{user.UserId}|O-{org.Id}" };
        org.MemberIdentity.Add(identity);
        user.Organizes.Add(identity);

        await _context.SaveChangesAsync();
        return org;
    }

    [HttpPost]
    public async Task<ActionResult> UpdateOrganize([FromBody] OrganizeModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var organize = await _context.Organizes.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (organize == null) return NotFound();

        organize.Update(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel[]>> GetOrganizeMember(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var org = await _context.Organizes.Include(x => x.MemberIdentity).ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return org?.MemberIdentity.Select(x => new UserModel() { Name = x.User.Name, UserId = x.UserId }).ToArray() ??
               [];
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> QuitOrganize(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        
        var organizeModel = await _context.Organizes.Include(model => model.MemberIdentity)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (organizeModel == null) return NotFound();

        var m = organizeModel.MemberIdentity.FirstOrDefault(x => x.UserId == member.UserId);
        if (m == null) return Ok();

        if (m.Identity == "President")
            _context.Organizes.Remove(organizeModel);
        else
            organizeModel.MemberIdentity.Remove(m);
        
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    #endregion

    #region 组织项目管理

    [HttpPost("{id}")]
    public async Task<ActionResult<ProjectModel>> CreateOrgProject(string id, [FromBody] ProjectModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => member.UserId == x.UserId);
        if (user == null) return NotFound();
        var org = await _context.Organizes.Include(x => x.MemberIdentity)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        model.Id = model.ToString().HashEncryption();
        model.Members.Add(new ProjectIdentity() { User = user, Identity = "Minister" });

        org.Projects.Add(model);
        await _context.SaveChangesAsync();

        return model;
    }

    #endregion

    #region 公告系统

    /// <summary>
    /// 查看最新公告
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AnnouncementModel>> LookAnnouncement(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Announcements)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();
        return org.Announcements.LastOrDefault() ?? new AnnouncementModel();
    }

    /// <summary>
    /// 查看所有公告
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<List<AnnouncementModel>>> LookAnnouncements(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(x => x.Announcements)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();
        return org.Announcements;
    }

    /// <summary>
    /// 添加公告
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<ActionResult> AddAnnouncement([FromBody] AnnouncementModel model, string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Announcements)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        var i = await _context.OrganizeIdentities.FirstOrDefaultAsync(x =>
            x.Key == $"U-{member.UserId}|O-{org.Id}");

        if (i?.Identity != "President") return NotFound();
        model.Id = model.ToString().HashEncryption();

        org.Announcements.Add(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<ActionResult> RemoveAnnouncement([FromBody] AnnouncementModel model, string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var i = await _context.OrganizeIdentities.FirstOrDefaultAsync(x =>
            x.Key == $"U-{member.UserId}|O-{id}");

        if (i?.Identity != "President") return NotFound();

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
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ResourceModel>>> GetResources(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
            return NotFound();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var org = await _context.Organizes.Include(organizeModel => organizeModel.Resources)
            .FirstOrDefaultAsync(x => x.Id == id);

        return org?.Resources.ToArray() ?? [];
    }

    /// <summary>
    /// 更新资源状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="resourceModel"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<ActionResult> UpdateResource(string id, ResourceModel resourceModel)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        _context.Entry(resourceModel).State = EntityState.Modified;

        var i = await _context.OrganizeIdentities.FirstOrDefaultAsync(x =>
            x.Key == $"U-{member.UserId}|O-{id}");

        if (i?.Identity != "President") return NotFound();

        await _context.SaveChangesAsync();

        return NoContent();
    }


    /// <summary>
    /// 添加资源
    /// </summary>
    /// <param name="resourceModel"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<ActionResult<ResourceModel>> AddResource(ResourceModel resourceModel, string id)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var org = await _context.Organizes.Include(organizeModel => organizeModel.Resources)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (org == null) return NotFound();

        var i = await _context.OrganizeIdentities.FirstOrDefaultAsync(x =>
            x.Key == $"U-{member.UserId}|O-{id}");

        if (i?.Identity != "President") return NotFound();

        resourceModel.Id = resourceModel.ToString().HashEncryption();
        org.Resources.Add(resourceModel);

        await _context.SaveChangesAsync();

        return resourceModel;
    }

    /// <summary>
    /// 删除资源
    /// </summary>
    /// <param name="id"></param>
    /// <param name="org"></param>
    /// <returns></returns>
    [HttpGet("{id}&{org}")]
    public async Task<ActionResult> DeleteResource(string id, string org)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var i = await _context.OrganizeIdentities.FirstOrDefaultAsync(x =>
            x.Key == $"U-{member.UserId}|O-{org}");

        if (i?.Identity != "President") return NotFound();

        var resourceModel = await _context.Resources.FindAsync(id);
        if (resourceModel == null)
            return NotFound();

        _context.Resources.Remove(resourceModel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    #endregion
}