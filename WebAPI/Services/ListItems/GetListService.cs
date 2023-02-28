using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Models;
using WebAPI.Models.DbModels;
using WebAPI.Models.Dtos;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class GetListService : IGetListService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetListService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ListItem GetListItem(string id)
        {
            var returnItem = unitOfWork.ListItem.GetById(id);

            if (returnItem != null) return returnItem;

            throw new Exception("Could not find ListItem with ID:" + id);
        }

        public IEnumerable<ListItem> GetByViewWithOptions(GetListDto dto)
        {
            List<ListItem> returnList = new();
            IQueryable<ListItem> query = null;

            switch (dto.CalendarView)
            {
                case CalendarView.Day:
                    query = unitOfWork.ListItem.Query(x => x.DueDate == dto.Date);
                    break;
                case CalendarView.Week:
                    var firstDayOfWeek = new DateTime(dto.Date.Year, dto.Date.Month, dto.Date.Day);
                    firstDayOfWeek.AddDays(-(int)firstDayOfWeek.DayOfWeek);
                    var lastDayOfWeek = firstDayOfWeek.AddDays(7);
                    query = unitOfWork.ListItem.Query(x => x.DueDate >= firstDayOfWeek && x.DueDate <= lastDayOfWeek);
                    break;
                case CalendarView.Month:
                    var firstDayOfMonth = new DateTime(dto.Date.Year, dto.Date.Month, 1);
                    var firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);
                    query = unitOfWork.ListItem.Query(x => x.DueDate >= firstDayOfMonth && x.DueDate <= firstDayOfNextMonth);
                    break;
            }
            if (query == null) throw new Exception("Logic Error in GetByDate");

            if (dto.ListTypeName != null)
            {
                query = unitOfWork.ListItem.Query(x => x.ListTypeName == dto.ListTypeName);

                if (dto.IgnoreCompleted)
                {
                    returnList = query.ToList().Where(x => !x.IsCompleted).ToList();
                }

            }

            if (returnList.Count == 0)
                returnList = query.ToList();

            return returnList;

        }





    }
}
