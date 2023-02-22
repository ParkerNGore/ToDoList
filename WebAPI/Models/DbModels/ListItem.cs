using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.DbModels.Interfaces;
using WebAPI.Models.Dtos;

namespace WebAPI.Models.DbModels
{
    public class ListItem : IDbItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public ListItem(CreateListItemDto dto)
        {
            this.DueDate = dto.DueDate;
            this.Title = dto.Title;
            this.Description = dto.Description;
            this.Frequency = dto.Frequency;
            this.Importance = dto.Importance;
            this.ListTypeName = dto.ListTypeName;
        }

    }
}
