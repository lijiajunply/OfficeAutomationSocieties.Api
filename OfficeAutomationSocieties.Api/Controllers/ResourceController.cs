using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.Share.DataModels;

namespace OfficeAutomationSocieties.Api.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class ResourceController : ControllerBase
{
    private readonly IDbContextFactory<OaContext> _factory;

    public ResourceController(IDbContextFactory<OaContext> factory)
    {
        _factory = factory;
    }

    // GET: api/Resource
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceModel>>> GetResources()
    {
        await using var _context = await _factory.CreateDbContextAsync();
        if (_context.Resources == null!)
        {
            return NotFound();
        }

        return await _context.Resources.ToListAsync();
    }

    // GET: api/Resource/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceModel>> GetResourceModel(string id)
    {
        await using var _context = await _factory.CreateDbContextAsync();
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

    // PUT: api/Resource/5
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutResourceModel(string id, ResourceModel resourceModel)
    {
        await using var _context = await _factory.CreateDbContextAsync();
        if (id != resourceModel.Id)
        {
            return BadRequest();
        }

        _context.Entry(resourceModel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(_context.Resources?.Any(e => e.Id == id)).GetValueOrDefault())
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Resource
    // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ResourceModel>> AddResource(ResourceModel resourceModel)
    {
        await using var _context = await _factory.CreateDbContextAsync();
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
            if ((_context.Resources?.Any(e => e.Id == resourceModel.Id)).GetValueOrDefault())
            {
                return Conflict();
            }

            throw;
        }

        return CreatedAtAction("GetResourceModel", new { id = resourceModel.Id }, resourceModel);
    }

    // DELETE: api/Resource/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResourceModel(string id)
    {
        await using var _context = await _factory.CreateDbContextAsync();
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