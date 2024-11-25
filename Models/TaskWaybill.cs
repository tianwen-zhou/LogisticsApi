public class TaskWaybill
{
    public int Id { get; set; } // 主键
    
    public int TaskId { get; set; }         // 外键，指向 Task

    public int WaybillId { get; set; }     // 外键，指向 Waybill

    public string Status { get; set; }     // 关联状态
}
