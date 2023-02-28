namespace WebAPI.Models.Dtos
{
    public class GetListDto
    {
        public DateTime Date { get; set; }
        public CalendarView CalendarView { get; set; }
        public bool IgnoreCompleted { get; set; }
        public string? ListTypeName { get; set; } = null;
    }
}
