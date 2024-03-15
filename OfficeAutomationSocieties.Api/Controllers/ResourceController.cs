using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

/// <summary>
/// 资源系统
/// </summary>
/// <param name="factory">数据库</param>
[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class ResourceController(IDbContextFactory<OaContext> factory) : ControllerBase
{
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceModel>>> GetResources()
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return NotFound();
        }

        return await _context.Resources.ToListAsync();
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

        var resourceModel = await _context.Resources.FindAsync(id);

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
    [HttpPost]
    public async Task<ActionResult<ResourceModel>> AddResource(ResourceModel resourceModel)
    {
        await using var _context = await factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return Problem("Entity set 'OaContext.Resources'  is null.");
        }

        _context.Resources.Add(resourceModel);
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

        return CreatedAtAction("GetResourceModel", new { id = resourceModel.Id }, resourceModel);
    }

    /// <summary>
    /// 删除资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
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
}