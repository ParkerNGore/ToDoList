using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Interfaces
{
    public interface IDbItem
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
