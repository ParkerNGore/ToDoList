using WebAPI.Models.Interfaces;

namespace WebAPI.Models
{
    public class ListType : IDbItem
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ListItem> ListItems { get; set; }
        public ListType() { }
    }
}
