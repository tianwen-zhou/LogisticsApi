namespace LogisticsApi.DTOs
{
    public class Location
    {
        public int Order { get; set; } 
        public string Name { get; set; } 
        public double Lat { get; set; } // 收件人纬度
        public double Lng { get; set; } // 收件人经度
    }
}

