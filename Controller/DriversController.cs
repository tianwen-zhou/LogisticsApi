using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsApi.Data;
using LogisticsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly LogisticsDbContext _context;

    public DriversController(LogisticsDbContext context)
    {
        _context = context;
    }

    // GET: api/Drivers?_start=0&_end=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers(
        [FromQuery] int _start = 0, 
        [FromQuery] int _end = 10)
    {
        // 获取总记录数
        var totalCount = await _context.Drivers.CountAsync();

        // 获取分页数据
        var drivers = await _context.Drivers
            .OrderBy(d => d.Id) // 按 ID 排序（React-Admin 需要稳定排序）
            .Skip(_start)       // 跳过前 _start 条记录
            .Take(_end - _start) // 获取 _end - _start 条记录
            .ToListAsync();

        // 添加 X-Total-Count 响应头
        HttpContext.Response.Headers.Add("X-Total-Count", totalCount.ToString());

        return Ok(drivers);
    }

    // GET: api/Drivers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Driver>> GetDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);

        if (driver == null)
        {
            return NotFound();
        }

        return driver;
    }

    // POST: api/Drivers
    [HttpPost]
    public async Task<ActionResult<Driver>> PostDriver(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
    }

    // PUT: api/Drivers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDriver(int id, Driver driver)
    {
        if (id != driver.Id)
        {
            return BadRequest();
        }

        _context.Entry(driver).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Drivers.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Drivers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null)
        {
            return NotFound();
        }

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
