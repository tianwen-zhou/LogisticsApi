using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsApi.Data;
using LogisticsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class PostalAreasController : ControllerBase
{
    private readonly LogisticsDbContext _context;

    public PostalAreasController(LogisticsDbContext context)
    {
        _context = context;
    }

    // GET: api/PostalAreas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostalArea>>> GetPostalAreas(
        [FromQuery] int _start = 0, 
        [FromQuery] int _end = 10)
    {
        var totalCount = await _context.PostalAreas.CountAsync();
        // 获取分页数据
        var postalAreas = await _context.PostalAreas
            .OrderBy(d => d.Id) // 按 ID 排序（React-Admin 需要稳定排序）
            .Skip(_start)       // 跳过前 _start 条记录
            .Take(_end - _start) // 获取 _end - _start 条记录
            .ToListAsync();

        // 返回结果
        return Ok(new
        {
            data = postalAreas,
            total = totalCount
        });
    }

    // GET: api/PostalAreas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PostalArea>> GetPostalArea(int id)
    {
        var postalArea = await _context.PostalAreas.FindAsync(id);

        if (postalArea == null)
        {
            return NotFound();
        }

        return postalArea;
    }

    // POST: api/PostalAreas
    [HttpPost]
    public async Task<ActionResult<PostalArea>> PostPostalArea(PostalArea postalArea)
    {
        _context.PostalAreas.Add(postalArea);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPostalArea), new { id = postalArea.Id }, postalArea);
    }

    // PUT: api/PostalAreas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPostalArea(int id, PostalArea postalArea)
    {
        if (id != postalArea.Id)
        {
            return BadRequest();
        }

        _context.Entry(postalArea).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PostalAreas.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/PostalAreas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostalArea(int id)
    {
        var postalArea = await _context.PostalAreas.FindAsync(id);
        if (postalArea == null)
        {
            return NotFound();
        }

        _context.PostalAreas.Remove(postalArea);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
