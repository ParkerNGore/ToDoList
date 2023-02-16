using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IGetAllListsByType
    {
        IEnumerable<ListItem> GetAllListItemsByType(string typeName);
    }
}
