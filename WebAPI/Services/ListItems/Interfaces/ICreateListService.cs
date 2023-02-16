using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface ICreateListService
    {
        ListItem Create(CreateListItemDto dto);
    }
}
