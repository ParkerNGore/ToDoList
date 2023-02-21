using WebAPI.Models.DbModels;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetAllListsByTypeService : IGetAllListsByType
    {
        public IEnumerable<ListItem> GetAllListItemsByType(string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
