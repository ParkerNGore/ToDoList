using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetAllListsService : IGetAllListsService
    {
        private readonly UnitOfWork unitOfWork;
        public GetAllListsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<ListItem> GetAllLists()
        {
            var returnList = unitOfWork.ListItem.GetAll();

            if (returnList == null) return new List<ListItem>();

            return returnList;
        }
    }
}
