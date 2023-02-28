using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IUpdateListService
    {
        ListItem UpdateListItem(ListItem item, bool isNewListType);
    }
}
