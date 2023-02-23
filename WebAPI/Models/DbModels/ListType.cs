using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.DbModels.Interfaces;

namespace WebAPI.Models.DbModels
{
    public class ListType : IDbItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public List<ListItem> ListItems { get; set; }
        public ListType() { }
        public ListType(string Name, string Description = "") 
        {
            this.Name = Name;
        }

    }
}
