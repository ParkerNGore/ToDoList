using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;
using WebAPI.Services.ListItems;
using WebAPI.Services.ListItems.Interfaces;
using WebAPI.Services.ListTypes.interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ListTypeController : ControllerBase
    {
        private readonly IGetListTypeService getListTypeService;

        public ListTypeController(IGetListTypeService getListTypeService)
        {
            this.getListTypeService = getListTypeService;
        }

        [HttpGet]
        public List<ListType> GetListTypes() => 
            getListTypeService.GetListTypes();
    }
}