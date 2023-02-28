using WebAPI.Models.DbModels;

namespace WebAPI.Services.ListTypes.interfaces
{
    public interface IGetListTypeService
    {
        List<ListType> GetListTypes();
    }
}