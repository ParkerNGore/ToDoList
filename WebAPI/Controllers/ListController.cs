using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;
using WebAPI.Services.ListItems;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListItemService service;

        public ListController(IListItemService listItemService)
        {
            this.service = listItemService;
        }

        [HttpPost]
        public ListItem Create(CreateListItemDto dto) =>
            service.Create(dto);

        [HttpPut("{isNewListType:bool}")]
        public ListItem Update(ListItem item, bool isNewListType) =>
            service.UpdateListItem(item, isNewListType);

        [HttpDelete("{id}")]
        public void Delete(string id) =>
        service.Delete(id);

        [HttpGet("{id}")]
        public ListItem GetListItem(string id) =>
            service.GetListItem(id);

        [HttpGet]
        public IEnumerable<ListItem> GetAllLists() => 
            service.GetAllLists();

        [HttpGet("{typeName}/{ignoreCompleted:bool}")]
        public IEnumerable<ListItem> FilterListItems(string typeName, bool ignoreCompleted) =>
            service.FilterListItems(typeName, ignoreCompleted);

        [HttpPost]
        public IEnumerable<ListItem> GetByViewWithOptions(GetListDto dto) => 
            service.GetByViewWithOptions(dto);

        


    }
}
