using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class FilterListItemsService : IFilterListItemsService
    {
        private readonly IUnitOfWork unitOfWork;
        public FilterListItemsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<ListItem> FilterListItems(string typeName, bool ignoreCompleted)
        {
            var returnList = unitOfWork.ListItem.Query(x => x.ListTypeName == typeName);
            //idk why this doesnt work as a query
            if (ignoreCompleted)
                returnList.Where(x => !x.IsCompleted);

            if (returnList == null) return new List<ListItem>();

            //works just fine like this tho. Probably some wacky sql translation >->
            List<ListItem> test = returnList.ToList();
            test = test.Where(x => !x.IsCompleted).ToList();
            return test;
        }
    }
}
