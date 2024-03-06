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

        if (_context.Projects == null!)
            return NotFound();

        return await _context.Projects.ToListAsync();
    }

    /// <summary>
    /// 加入项目组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> JoinProject([FromBody] string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.Any(x => x.UserId == member.UserId)) return Ok();

        project.Members.Add(member);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProject()
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var project = new ProjectModel();
        project.Members.Add(member);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return Ok();
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

        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != member.UserId)) return Ok();

        project.Members.Remove(member);
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
    /// 甘特图管理
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> AddGantt(string id, GanttModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var project = await _context.Projects.Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != user.UserId)) return NotFound();

        model.Id = $"GanttModel is {{{model.User}:{model.StartTime}-{model.EndTime}:{model.ToDo}}} Other is Private"
            .HashEncryption();
        project.Gantt += $"{model}\n";

        await _context.SaveChangesAsync();
        return Ok();
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

        var project = await _context.Projects.Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != user.UserId)) return NotFound();

        var list = project.Gantt.Split('\n').Select(x => new GanttModel(x));
        var gantt = list.FirstOrDefault(x => x.Id == ganttId);
        if (gantt == null) return NoContent();

        project.Gantt = project.Gantt.Replace($"{gantt}\n", "");
        await _context.SaveChangesAsync();
        return Ok();
    }
}