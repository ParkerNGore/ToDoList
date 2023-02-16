using WebAPI.Models.DbModels.Interfaces;

namespace WebAPI.Models.DbModels
{
    public class ListItem : IDbItem
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RepeatFrequency Frequency { get; set; }
        public Importance Importance { get; set; }

        #region Relationships
        public string ListTypeName { get; set; }
        public ListType Type { get; set; }
        #endregion Relationships

        public ListItem() { }
    }
}
