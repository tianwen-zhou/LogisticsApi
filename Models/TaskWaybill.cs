
namespace LogisticsApi.Models
{
    public class TaskWaybill
    {
        public int Id { get; set; } // 主键

        public string TaskNumber { get; set; } // 外键，指向 Task

        public string WaybillNumber { get; set; } // 外键，指向 Waybill

        public int Status { get; set; } // 关联状态

        // 新增字段
        public int Order { get; set; } // 排序字段，用于指定任务中的运单顺序
    }

}
