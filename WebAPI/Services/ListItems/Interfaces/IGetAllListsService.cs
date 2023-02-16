using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IGetAllListsService
    {
        IEnumerable<ListItem> GetAllLists();
    }
}
