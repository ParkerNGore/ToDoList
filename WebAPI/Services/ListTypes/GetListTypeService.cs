using WebAPI.Models.DbModels;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListTypes.interfaces;

namespace WebAPI.Services.ListTypes
{
    public class GetListTypeService : IGetListTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetListTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<ListType> GetListTypes()
        {
            return unitOfWork.ListType.GetAll().ToList();
        }
    }
}
