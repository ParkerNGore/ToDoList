using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IFilterListItemsService
    {
        IEnumerable<ListItem> FilterListItems(string typeName, bool ignoreCompleted);
    }
}
