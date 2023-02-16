using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IDeleteListService
    {
        ListItem Delete(string id);
    }
}
