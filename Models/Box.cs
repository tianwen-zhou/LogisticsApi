namespace LogisticsApi.Models
{
    public class Box
    {
        public int Id { get; set; }
        public string BoxNumber { get; set; }
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
