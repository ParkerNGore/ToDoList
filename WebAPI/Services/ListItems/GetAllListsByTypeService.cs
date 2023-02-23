using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetAllListsByTypeService : IGetAllListsByType
    {
        private readonly UnitOfWork unitOfWork;
        public GetAllListsByTypeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<ListItem> GetAllListItemsByType(string typeName)
        {
            var returnList = unitOfWork.ListItem.Query(x => x.ListTypeName == typeName);

            if (returnList == null) return new List<ListItem>();

            return returnList;
        }
    }
}
