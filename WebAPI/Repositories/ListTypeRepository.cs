using WebAPI.Data;
using WebAPI.Models.DbModels;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories
{
    public class ListTypeRepository: GenericRepository<ListType>, IListTypeRepository
    {
        public ListTypeRepository(DataContext context) : base(context) { }

    }
}
