using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetAllListsService : IGetAllListsService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetAllListsService(IUnitOfWork unitOfWork)
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
