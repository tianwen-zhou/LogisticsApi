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

        [HttpPost("GenerateTaskByRoute/{routeCode}")]
        public async Task<ActionResult<DeliveryTask>> GenerateTaskByRoute(string routeCode)
        {
            // 确认是否存在给定 RouteCode 的数据
            var waybills = await _context.Waybills
                .Where(wb => wb.RouteCode == routeCode  && wb.Status == "1")
                .ToListAsync();

            if (!waybills.Any())
            {
                return NotFound($"No WayBills found for RouteCode: {routeCode}");
            }

            // 创建新任务
            var newTask = new DeliveryTask
            {
                TaskNumber = Guid.NewGuid().ToString(), // 生成唯一任务号
                TaskType = "1", // 自定义任务类型
                TaskStatus = "0", // 默认任务状态
                AssignedTo = "Unassigned", // 默认分配状态
                CreatedTime = DateTime.UtcNow,
                UpdatedTime = DateTime.UtcNow
            };

            _context.DeliveryTasks.Add(newTask);
            await _context.SaveChangesAsync();

            // 创建新任务和运单关联数据
                // 遍历 Waybills 数据并创建对应的 TaskWaybills
            foreach (var waybill in waybills)
            {
                // 检查是否已经存在对应的 TaskWaybill，避免重复生成
                bool exists = _context.TaskWaybills.Any(tw => tw.WaybillNumber == waybill.WaybillNumber 
                && tw.TaskNumber == newTask.TaskNumber);
                if (exists)
                {
                    continue;
                }

                var taskWaybill = new TaskWaybill
                {
                    WaybillNumber = waybill.WaybillNumber,
                    TaskNumber = newTask.TaskNumber, 
                    Order = 0,
                    Status = 0
                };

                _context.TaskWaybills.Add(taskWaybill);
            }

            // 保存更改到数据库
            await _context.SaveChangesAsync();

            return Ok("TaskWaybills created successfully.");
        }

    
    }
}
