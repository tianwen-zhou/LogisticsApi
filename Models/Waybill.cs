namespace LogisticsApi.Models
{
    public class Waybill
    {
        public int Id { get; set; } // 主键
        public string WaybillNumber { get; set; } // 运单号
        public string Sender { get; set; } // 寄件人
        public string SenderAddress { get; set; } // 寄件人地址
        public string SenderPhone { get; set; } // 寄件人电话
        public string Recipient { get; set; } // 收件人
        public string RecipientAddress { get; set; } // 收件人地址
        public string RecipientPhone { get; set; } // 收件人电话
        public string PostCode { get; set; } // 邮编
        public string RouteCode { get; set; } // 路径代码
        public decimal ParcelValue { get; set; } // 包裹价值
        public string DeliveryTime { get; set; } // 配送时效

        // 新增字段
        public double SenderLatitude { get; set; } // 寄件人纬度
        public double SenderLongitude { get; set; } // 寄件人经度
        public double RecipientLatitude { get; set; } // 收件人纬度
        public double RecipientLongitude { get; set; } // 收件人经度
    }

}
