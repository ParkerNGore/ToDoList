using System.Xml.Linq;
using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;
using WebAPI.Repositories;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class CreateListItemService : ICreateListService
    {
        private readonly UnitOfWork unitOfWork;
        public CreateListItemService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ListItem Create(CreateListItemDto dto)
        {
            ListType? listType = unitOfWork.ListType.Query(x => x.Name == dto.ListTypeName).FirstOrDefault();
            if (listType == null && dto.IsNewListType)
                listType = new(dto.ListTypeName);

            ListItem newItem = new(dto, listType);

            try
            {
                unitOfWork.ListItem.Add(newItem);
                unitOfWork.Complete();
            }
            catch (Exception e)
            {

                throw new Exception($"Failed to Add List Item to Database. See Exception {e}. With Inner Exception {e.Message}.");
            }

            return newItem;
        }
    }
}
