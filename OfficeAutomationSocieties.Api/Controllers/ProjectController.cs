using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 项目管理系统
/// </summary>
/// <param name="factory"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="environment"></param>
[Authorize]
[TokenActionFilter]
[Route("api/[controller]/[action]")]
[ApiController]
[Produces("application/json")]
[Consumes("application/json", "multipart/form-data")] //此处为新增
public class ProjectController(
    IDbContextFactory<OaContext> factory,
    IHttpContextAccessor httpContextAccessor,
    IWebHostEnvironment environment)
    : ControllerBase
{
    /// <summary>
    /// 获取所有项目
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "President")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjects()
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var org = await _context.Organizes.Include(x => x.Projects).FirstOrDefaultAsync(x => x.Id == member.NowOrgId);
        if (org == null) return NotFound();

        return org.Projects;
    }

    /// <summary>
    /// 加入项目组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> JoinProject(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.Any(x => x.UserId == member.UserId)) return Ok();

        project.Members.Add(user);
        await _context.SaveChangesAsync();
        return project;
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ProjectModel>> CreateProject([FromBody] ProjectModel project)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        project.Id = project.ToString().HashEncryption();
        project.Members.Add(user);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }


    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> RemoveProject(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != member.UserId)) return Ok();

        project.Members.Remove(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// 添加文件
    /// </summary>
    /// <param name="id"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> AddFile(string id, IFormFile file)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var project = await _context.Projects.Include(x => x.Members).Include(x => x.Files)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != user.UserId)) return NotFound();

        var filePath = environment.WebRootPath + "\\ProjectFiles";
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        var fileName = $"{project.Id}/{Convert.ToBase64String(
            System.Text.Encoding.Default.GetBytes($"{DateTime.Now:s}{Guid.NewGuid().ToString()}"))}/{file.FileName}";
        var saveFilePath = Path.Combine(filePath, fileName);

        await using var saveStream = new FileStream(saveFilePath, FileMode.OpenOrCreate);
        await file.CopyToAsync(saveStream);

        var pwd = $"{project.Id}{DateTime.Now:s}{Guid.NewGuid().ToString()}{file.FileName}".HashEncryption();
        project.Files.Add(new FileModel() { Path = fileName, Id = pwd });
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<GanttModel>> AddGantt(string id, GanttModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var project = await _context.Projects.Include(x => x.Members).Include(x => x.GanttList)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != user.UserId)) return NotFound();

        if (project.GanttList.Any(x => x.Id == model.Id)) return NoContent();
        model.Id = $"GanttModel is {{{model.User}:{model.StartTime}-{model.EndTime}:{model.ToDo}}} Other is Private"
            .HashEncryption();
        project.GanttList.Add(model);

        await _context.SaveChangesAsync();
        return model;
    }


    /// <summary>
    /// 删除计划
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ganttId"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> RemoveGantt(string id, string ganttId)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var project = await _context.Projects.Include(x => x.Members).Include(projectModel => projectModel.GanttList)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != user.UserId)) return NotFound();

        var gantt = project.GanttList.FirstOrDefault(x => x.Id == ganttId);
        if (gantt == null) return NoContent();

        project.GanttList.Remove(gantt);
        await _context.SaveChangesAsync();
        return Ok();
    }
}