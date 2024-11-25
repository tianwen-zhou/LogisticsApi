namespace LogisticsApi.Models
{
    public class Driver
    {
        public int Id { get; set; } // 主键
        public string Name { get; set; } // 司机姓名
        public string Phone { get; set; } // 电话号码
        public string Gender { get; set; } // 性别（如 "Male", "Female", "Other"）
        public string LicensePhoto { get; set; } // 驾照照片（存储文件路径或 URL）
        public string CompanyName { get; set; } // 公司名称
    }
}
