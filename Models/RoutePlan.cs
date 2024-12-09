namespace LogisticsApi.Models
{
        public class RoutePlan
    {
        public int Id { get; set; } // 主键
        public string PlanName { get; set; } // 路线计划名称
        public int DriverId { get; set; } // 驾驶员ID

        // 新增字段
        public string TaskNumber { get; set; } // 任务号
        public int Version { get; set; } // 版本号
        public string Status { get; set; } // 状态 (如 "Pending", "InProgress", "Completed")
    }

}
