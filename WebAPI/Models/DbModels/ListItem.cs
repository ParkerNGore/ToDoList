using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.DbModels.Interfaces;
using WebAPI.Models.Dtos;

namespace WebAPI.Models.DbModels
{
    public class ListItem : IDbItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public RepeatFrequency Frequency { get; set; }
        public Importance Importance { get; set; }
        public bool IsAllDay { get; set; } = true;
        public bool IsCompleted { get; set; } = false;
        #region Relationships
        public string ListTypeName { get; set; }
        public ListType Type { get; set; }
        #endregion Relationships

        public ListItem() { }

        public ListItem(CreateListItemDto dto, ListType type)
        {
            this.DueDate = dto.DueDate;
            this.Title = dto.Title;
            this.Description = dto.Description;
            this.Frequency = dto.Frequency;
            this.Importance = dto.Importance;
            this.Type = type;
            this.CreatedDate = DateTime.Now;
            this.IsAllDay = dto.IsAllDay;
        }

        // It's dumb that this constructor is dumb and resets Created Date
        //public ListItem Update(ListItem item, ListType? type = null)
        //{
        //    this.Title = item.Title;
        //    this.Description = item.Description;
        //    this.Frequency = item.Frequency;
        //    this.Importance = item.Importance;

        //    if (type != null)
        //        this.Type = type;

        //    LastUpdatedDate = DateTime.Now;

        //    return this;
        //}

    }
}
