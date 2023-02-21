using WebAPI.Data;
using WebAPI.Models.DbModels;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class ListItemRepository: GenericRepository<ListItem>, IListItemRepository
    {
        public ListItemRepository(DataContext context) : base(context) { }
    }
}
