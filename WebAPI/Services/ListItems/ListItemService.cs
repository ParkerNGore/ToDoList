using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class ListItemService : IListItemService
    {
        private readonly IGetListService getListService;
        private readonly IGetAllListsService getAllListsService;
        private readonly IGetAllListsByType getAllListsByType;
        private readonly ICreateListService createListService;
        private readonly IDeleteListService deleteListService;
        private readonly IUpdateListService updateListService;

        public ListItemService(IGetListService getListService, IGetAllListsService getAllListsService, IGetAllListsByType getAllListsByType, ICreateListService createListService, IDeleteListService deleteListService, IUpdateListService updateListService)
        {
            this.getListService = getListService;
            this.getAllListsService = getAllListsService;
            this.getAllListsByType = getAllListsByType;
            this.createListService = createListService;
            this.deleteListService = deleteListService;
            this.updateListService = updateListService;
        }

        public ListItem GetListItem(string id) => getListService.GetListItem(id);
        public IEnumerable<ListItem> GetAllLists() => getAllListsService.GetAllLists();
        public ListItem Create(CreateListItemDto dto) => createListService.Create(dto);
        public void Delete(string id) => deleteListService.Delete(id);
        public ListItem UpdateListItem(ListItem item) => updateListService.UpdateListItem(item);
        public IEnumerable<ListItem> GetAllListItemsByType(string typeName) => getAllListsByType.GetAllListItemsByType(typeName);
    }
}
