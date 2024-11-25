namespace LogisticsApi.Models
{
    public class PostalArea
    {
        public int Id { get; set; } // Primary Key
        public string City { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string RouteCode { get; set; }
        public string Zone { get; set; }
        public bool RemoteArea { get; set; } // True/False to indicate remote area
        public string DeliveryServiceProvider { get; set; }
        public string ActivationPlan { get; set; }
        public string LMShippingStation { get; set; }
        public string LMDeliveryStation { get; set; }
    }
}
