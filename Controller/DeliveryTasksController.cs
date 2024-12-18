using System;
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
    public class DeliveryTasksController : ControllerBase
    {
        private readonly LogisticsDbContext _context;

        public DeliveryTasksController(LogisticsDbContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryTasks
        [HttpGet]
        public async Task<ActionResult> GetDeliveryTasks([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            if (page < 1 || perPage < 1)
            {
                return BadRequest("Page and perPage must be greater than 0.");
            }

            // 查询数据总数
            var total = await _context.DeliveryTasks.CountAsync();

            // 计算分页数据
            var tasks = await _context.DeliveryTasks
                .OrderBy(t => t.Id) // 默认按 ID 排序，可根据需要更改字段
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();

            // 返回结果
            return Ok(new
            {
                data = tasks,
                total = total
            });
        }


        // GET: api/DeliveryTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryTask>> GetDeliveryTask(int id)
        {
            var deliveryTask = await _context.DeliveryTasks.FindAsync(id);

            if (deliveryTask == null)
            {
                return NotFound();
            }

            return deliveryTask;
        }

        // POST: api/DeliveryTasks
        [HttpPost]
        public async Task<ActionResult<DeliveryTask>> PostDeliveryTask(DeliveryTask deliveryTask)
        {
            // 设置创建时间
            deliveryTask.CreatedTime = DateTime.UtcNow;
            deliveryTask.UpdatedTime = DateTime.UtcNow;

            _context.DeliveryTasks.Add(deliveryTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeliveryTask), new { id = deliveryTask.Id }, deliveryTask);
        }

        // PUT: api/DeliveryTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryTask(int id, DeliveryTask deliveryTask)
        {
            if (id != deliveryTask.Id)
            {
                return BadRequest();
            }

            // 设置更新时间
            deliveryTask.UpdatedTime = DateTime.UtcNow;

            _context.Entry(deliveryTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTaskExists(id))
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

        // DELETE: api/DeliveryTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryTask(int id)
        {
            var deliveryTask = await _context.DeliveryTasks.FindAsync(id);
            if (deliveryTask == null)
            {
                return NotFound();
            }

            _context.DeliveryTasks.Remove(deliveryTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryTaskExists(int id)
        {
            return _context.DeliveryTasks.Any(e => e.Id == id);
        }
    }
}
