using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LogisticsApi.Data;
using Microsoft.EntityFrameworkCore;
using LogisticsApi.DTOs;

namespace LogisticsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {

        private readonly LogisticsDbContext _context;

        private readonly HttpClient _httpClient;
        private readonly string _googleApiKey = "YOUR_GOOGLE_API_KEY";

        public RouteController(HttpClient httpClient, LogisticsDbContext context)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost("getRoute")]
        public async Task<IActionResult> GetRoute([FromBody] List<string> destinations)
        {
            if (destinations == null || destinations.Count < 2)
                return BadRequest("At least two addresses are required.");

            var origin = destinations[0]; // First address as the origin
            var waypoints = string.Join("|", destinations.GetRange(1, destinations.Count - 1)); // Remaining addresses as waypoints

            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={origin}&waypoints={waypoints}&key={_googleApiKey}";

            var response = await _httpClient.GetStringAsync(url);

            if (string.IsNullOrEmpty(response))
                return BadRequest("Error retrieving route from Google Maps API.");

            return Ok(response); // Return the Google Maps Directions API response
        }

        [HttpGet("getTaskRoute/{taskNumber}")]
        public async Task<IActionResult> getTaskRoute(string taskNumber)
        {
            if (taskNumber == null)
                return BadRequest("taskNumber must be not null.");

            try
            {
                // 从 TaskWaybills 表中获取对应的记录
                var taskWaybills = await _context.TaskWaybills
                    .Where(tw => tw.TaskNumber == taskNumber)
                    .ToListAsync();

                if (taskWaybills == null || taskWaybills.Count == 0)
                    return NotFound("No TaskWaybills found for the given TaskNumber.");

                // 根据每个 TaskWaybill 的 WaybillId 去 Waybill 表中获取对应详情
                var waybillIds = taskWaybills.Select(tw => tw.WaybillNumber).ToList();

                var waybillDetails = await _context.Waybills
                    .Where(wb => waybillIds.Contains(wb.WaybillNumber))
                    .ToListAsync();

                if (waybillDetails == null || waybillDetails.Count == 0)
                    return NotFound("No Waybill details found for the associated WaybillIds.");

                // 创建组合数据结构
                var taskWaybillCombine = new TaskWaybillCombine
                {
                    TaskNumber = taskNumber,
                    Locations = taskWaybills
                        .Join(waybillDetails,
                            taskWaybill => taskWaybill.WaybillNumber,
                            waybill => waybill.WaybillNumber,
                            (taskWaybill, waybill) => new Location // 明确指定为 Location 类型
                            {
                                Order = taskWaybill.Order,
                                Name = waybill.Recipient,
                                Lat = waybill.RecipientLatitude,
                                Lng = waybill.RecipientLongitude
                            })
                        .ToList()
                };
                return Ok(taskWaybillCombine);
            }
            catch (Exception ex)
            {
                // 捕获异常并返回 500 错误
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
