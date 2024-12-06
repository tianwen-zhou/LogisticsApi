using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogisticsApi.Data;
using LogisticsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly LogisticsDbContext _context;

    public WaybillsController(LogisticsDbContext context)
    {
        _context = context;
    }

    // GET: api/Waybills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Waybill>>> GetWaybills()
    {
        return await _context.Waybills.ToListAsync();
    }

    // GET: api/Waybills/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Waybill>> GetWaybill(int id)
    {
        var Waybill = await _context.Waybills.FindAsync(id);

        if (Waybill == null)
        {
            return NotFound();
        }

        return Waybill;
    }

    // POST: api/Waybills
    [HttpPost]
    public async Task<ActionResult<Waybill>> PostWaybill(Waybill Waybill)
    {
        _context.Waybills.Add(Waybill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWaybill), new { id = Waybill.Id }, Waybill);
    }

    // PUT: api/Waybills/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWaybill(int id, Waybill Waybill)
    {
        if (id != Waybill.Id)
        {
            return BadRequest();
        }

        _context.Entry(Waybill).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Waybills.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Waybills/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaybill(int id)
    {
        var Waybill = await _context.Waybills.FindAsync(id);
        if (Waybill == null)
        {
            return NotFound();
        }

        _context.Waybills.Remove(Waybill);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
