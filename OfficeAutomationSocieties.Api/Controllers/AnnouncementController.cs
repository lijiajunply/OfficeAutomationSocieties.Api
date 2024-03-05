using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

[Authorize(Roles = "President")]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IDbContextFactory<OaContext> _factory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AnnouncementController(IDbContextFactory<OaContext> factory,IHttpContextAccessor httpContextAccessor)
    {
        _factory = factory;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<ActionResult> AddAnnouncement([FromBody] AnnouncementModel model)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        var member = _httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        _context.Announcements.Add(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> RemoveAnnouncement([FromBody] AnnouncementModel model)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        var member = _httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        _context.Announcements.Remove(model);
        await _context.SaveChangesAsync();
        return Ok();
    }
}