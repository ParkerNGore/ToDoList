using WebAPI.Models.DbModels;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class DeleteListItemService : IDeleteListService
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteListItemService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Delete(string id)
        {
            ListItem itemToDelete = unitOfWork.ListItem.GetById(id);

            try
            {
                unitOfWork.ListItem.Remove(itemToDelete);
                unitOfWork.Complete();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to Delete List Item to Database. See Exception {e}. With Inner Exception {e.Message}.");
            }
        }
    }
}
