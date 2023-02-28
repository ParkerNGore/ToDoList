using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Repositories;
using WebAPI.Services.ListItems;
using WebAPI.Services.ListItems.Interfaces;

namespace UnitTests.Builders
{
    public static class ListItemServiceBuilder
    {
        public static ListItemService Build()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var getAllListsService = new GetAllListsService(unitOfWork);
            var getAllListsByTypeService = new FilterListItemsService(unitOfWork);
            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService(unitOfWork);
            var deleteListItemService = new DeleteListItemService(unitOfWork);
            var getListItemService = new GetListService(unitOfWork);

            return new ListItemService(getListItemService, getAllListsService, getAllListsByTypeService, createListItemService, deleteListItemService, updateListItemService);
        }
    }
}
