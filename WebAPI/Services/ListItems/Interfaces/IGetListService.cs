using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IGetListService
    {
        ListItem GetListItem(string id);
        IEnumerable<ListItem> GetByViewWithOptions(GetListDto dto);
    }
}
