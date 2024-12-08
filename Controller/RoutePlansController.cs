using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsApi.Data;
using LogisticsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutePlansController : ControllerBase
    {
        private readonly LogisticsDbContext _context;

        public RoutePlansController(LogisticsDbContext context)
        {
            _context = context;
        }

        // GET: api/RoutePlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoutePlan>>> GetRoutePlans()
        {
            return await _context.RoutePlans.ToListAsync();
        }

        // GET: api/RoutePlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoutePlan>> GetRoutePlan(int id)
        {
            var routePlan = await _context.RoutePlans.FindAsync(id);

            if (routePlan == null)
            {
                return NotFound();
            }

            return routePlan;
        }

        // POST: api/RoutePlans
        [HttpPost]
        public async Task<ActionResult<RoutePlan>> PostRoutePlan(RoutePlan routePlan)
        {
            _context.RoutePlans.Add(routePlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoutePlan), new { id = routePlan.Id }, routePlan);
        }

        // PUT: api/RoutePlans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutePlan(int id, RoutePlan routePlan)
        {
            if (id != routePlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(routePlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutePlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/RoutePlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoutePlan(int id)
        {
            var routePlan = await _context.RoutePlans.FindAsync(id);
            if (routePlan == null)
            {
                return NotFound();
            }

            _context.RoutePlans.Remove(routePlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoutePlanExists(int id)
        {
            return _context.RoutePlans.Any(e => e.Id == id);
        }
    }
}
