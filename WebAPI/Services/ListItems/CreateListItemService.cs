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
            ListType? listType = dto.IsNewListType ? 
                new() { Name = dto.ListTypeName } : 
                unitOfWork.ListType.Query(x => x.Name == dto.ListTypeName).FirstOrDefault();

            if (listType == null) throw new Exception("Invalid List Type.");

            ListItem newItem = new(dto, listType);

            try
            {
                unitOfWork.ListItem.Add(newItem);
                unitOfWork.Complete();
            }
            catch (Exception e)
            {

                throw;
            }

            return newItem;
        }
    }
}
