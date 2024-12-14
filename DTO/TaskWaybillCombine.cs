
namespace LogisticsApi.DTOs
{
    public class TaskWaybillCombine
    {
        public string TaskNumber { get; set; } // 外键，指向 Task

        public List<Location> Locations { get; set; } // 外键，指向 Task

    }

}
