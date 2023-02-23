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
            ListItem newItem = new(dto);

            if (dto.IsNewListType)
                newItem.Type = new()
                {
                    Name = dto.ListTypeName,
                };

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
