using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 公告管理系统
/// </summary>
/// <param name="factory"></param>
/// <param name="httpContextAccessor"></param>
[Authorize(Roles = "President")]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
public class AnnouncementController(IDbContextFactory<OaContext> factory, IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    /// <summary>
    /// 添加公告
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> AddAnnouncement([FromBody] AnnouncementModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        _context.Announcements.Add(model);
        await _context.SaveChangesAsync();
        return Ok();
    }


    /// <summary>
    /// 删除公告
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
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
}