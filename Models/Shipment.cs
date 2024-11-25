namespace LogisticsApi.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public int PostalAreaId { get; set; }
        public PostalArea PostalArea { get; set; }
    }
}
