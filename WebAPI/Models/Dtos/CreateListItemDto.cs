namespace WebAPI.Models.Dtos
{
    public class CreateListItemDto
    {
        public DateTime DueDate { get; set; }
        public bool IsAllDay { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RepeatFrequency Frequency { get; set; } = RepeatFrequency.Never;
        public Importance Importance { get; set; } = Importance.Normal;
        public string ListTypeName { get; set; }
        public bool IsNewListType { get; set; } = false;
    }
}
