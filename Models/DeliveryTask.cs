using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogisticsApi.Models
{
    public class DeliveryTask
    {
        [Key] // 显式指定主键
        public int Id { get; set; } // 主键

        [Required] // 任务编号必填
        public string TaskNumber { get; set; } // 任务编号

        public string TaskType { get; set; } // 任务类型
        public string AssignedTo { get; set; } // 负责人
        public string TaskStatus { get; set; } // 任务状态

        [Required]
        public DateTime CreatedTime { get; set; } // 创建时间

        public DateTime UpdatedTime { get; set; } // 更新时间

    }
}
