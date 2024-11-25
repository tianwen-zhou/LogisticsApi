namespace LogisticsApi.Models
{
    public class MergeTask
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int[] BoxIds { get; set; }
    }
}
