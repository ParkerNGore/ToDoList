using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IGetListService
    {
        ListItem GetListItem(int id);
    }
}
