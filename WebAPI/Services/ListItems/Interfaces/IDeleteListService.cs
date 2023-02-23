using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListItems.Interfaces
{
    public interface IDeleteListService
    {
        void Delete(string id);
    }
}
