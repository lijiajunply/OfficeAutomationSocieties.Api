using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 项目管理系统
/// </summary>
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
    /// 获取用户全部项目内容
    /// </summary>
    /// <returns>用户全部项目</returns>
    [HttpGet]
    public async Task<ActionResult<ProjectModel[]>> GetUserProjects()
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var userModel = await _context.Users.Include(x => x.Projects)
            .ThenInclude(projectIdentity => projectIdentity.Project).ThenInclude(x => x.GanttList).AsSplitQuery()
            .FirstOrDefaultAsync(x => x.UserId == member.UserId);

        return userModel?.Projects.Select(x => x.Project).ToArray() ?? [];
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel[]>> GetProjectMember(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var proj = await _context.Projects.Include(x => x.Members).ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return proj?.Members.Select(x => new UserModel() { Name = x.User.Name, UserId = x.UserId }).ToArray() ?? [];
    }

    /// <summary>
    /// 由id获取项目内容
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProject(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var proj = await _context.Projects.Include(x => x.GanttList).Include(x => x.Files).Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);

        return proj ?? new ProjectModel();
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

        Console.WriteLine(_context.ProjectIdentities.Count());
        _context.ProjectIdentities.Add(new ProjectIdentity()
            { User = user, Project = project, Key = _context.ProjectIdentities.Count() });
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

        var i= _context.ProjectIdentities.Count();
        _context.Projects.Add(project);
        _context.ProjectIdentities.Add(new ProjectIdentity()
            { User = user, Identity = "Minister", Project = project, Key = i });
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

        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NotFound();

        var identity = project.Members.FirstOrDefault(x => x.UserId == member.UserId);
        if (identity?.Identity != "Minister") return NotFound();

        project.Members.Clear();
        _context.Remove(project);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> QuitProject(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();
        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NotFound();

        var m = project.Members.FirstOrDefault(x => x.UserId == member.UserId);
        if (m == null) return Ok();

        project.Members.Remove(m);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> UpdateProject([FromBody] ProjectModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var project = await _context.Projects.Include(projectModel => projectModel.Members)
            .FirstOrDefaultAsync(x => x.Id == model.Id);
        if (project == null) return NotFound();

        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var identity = project.Members.FirstOrDefault(x => x.UserId == member.UserId);
        if (identity?.Identity != "Minister") return NotFound();

        project.Update(model);
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
        var member = httpContextAccessor.HttpContext?.User.GetUser();
        if (member == null) return NotFound();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == member.UserId);
        if (user == null) return NotFound();

        var project = await _context.Projects.Include(x => x.Members).Include(x => x.GanttList)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (project == null) return NoContent();
        if (project.Members.All(x => x.UserId != member.UserId)) return NotFound();

        model.User = user;
        model.Id = model.ToString().HashEncryption();

        project.GanttList.Add(model);

        await _context.SaveChangesAsync();
        return model;
    }

    [HttpPost]
    public async Task<ActionResult> UpdateGantt([FromBody] GanttModel model)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var gantt = await _context.GanttList.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (gantt == null) return NotFound();
        gantt.Update(model);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>
    /// 删除计划
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> RemoveGantt(string id)
    {
        await using var _context = await factory.CreateDbContextAsync();
        var user = httpContextAccessor.HttpContext?.User.GetUser();
        if (user == null) return NotFound();

        var gantt = await _context.GanttList.FirstOrDefaultAsync(x => x.Id == id);
        if (gantt == null) return NoContent();

        _context.GanttList.Remove(gantt);
        await _context.SaveChangesAsync();
        return Ok();
    }
}