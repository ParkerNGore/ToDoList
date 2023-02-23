using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetListService : IGetListService
    {
        private readonly UnitOfWork unitOfWork;
        public GetListService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ListItem GetListItem(string id)
        {
            var returnItem = unitOfWork.ListItem.GetById(id);

            if(returnItem != null) return returnItem;

            throw new Exception("Could not find ListItem with ID:" + id);
        }
    }
}
