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
    public class TaskWaybillsController : ControllerBase
    {
        private readonly LogisticsDbContext _context;

        public TaskWaybillsController(LogisticsDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskWaybills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskWaybill>>> GetTaskWaybills()
        {
            return await _context.TaskWaybills.ToListAsync();
        }

        // GET: api/TaskWaybills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskWaybill>> GetTaskWaybill(int id)
        {
            var taskWaybill = await _context.TaskWaybills.FindAsync(id);

            if (taskWaybill == null)
            {
                return NotFound();
            }

            return taskWaybill;
        }

        // POST: api/TaskWaybills
        [HttpPost]
        public async Task<ActionResult<TaskWaybill>> PostTaskWaybill(TaskWaybill taskWaybill)
        {
            _context.TaskWaybills.Add(taskWaybill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskWaybill), new { id = taskWaybill.Id }, taskWaybill);
        }

        // PUT: api/TaskWaybills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskWaybill(int id, TaskWaybill taskWaybill)
        {
            if (id != taskWaybill.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskWaybill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskWaybillExists(id))
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

        // DELETE: api/TaskWaybills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskWaybill(int id)
        {
            var taskWaybill = await _context.TaskWaybills.FindAsync(id);
            if (taskWaybill == null)
            {
                return NotFound();
            }

            _context.TaskWaybills.Remove(taskWaybill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskWaybillExists(int id)
        {
            return _context.TaskWaybills.Any(e => e.Id == id);
        }
    }
}