using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebAPI.Models;
using WebAPI.Models.DbModels;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems.Interfaces;

namespace WebAPI.Services.ListItems
{
    public class UpdateListItemService : IUpdateListService
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdateListItemService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ListItem UpdateListItem(ListItem item, bool isNewListType)
        {
            var itemFromDb = unitOfWork.ListItem.Query(x => x.Id == item.Id).Include(x => x.Type).FirstOrDefault();

            if (item.ListTypeName != itemFromDb.ListTypeName)
            {
                var listType = unitOfWork.ListType.Query(x => x.Name == item.ListTypeName).FirstOrDefault();
                if (listType != null)
                    itemFromDb.Type = listType;
                else if(isNewListType)
                {
                    itemFromDb.Type = new()
                    {
                        Name = item.ListTypeName,
                        Description = "",
                    };
                    itemFromDb.ListTypeName = item.ListTypeName;
                } else
                    throw new Exception($"List Type with Name: {item.ListTypeName} does not exist in Database. If you want to create this ListType please select the corresponding Checkbox.");
            }
            itemFromDb.Title = item.Title;
            itemFromDb.Description = item.Description;
            itemFromDb.Frequency = item.Frequency;
            itemFromDb.Importance = item.Importance;
            itemFromDb.LastUpdatedDate = DateTime.Now;
            itemFromDb.DueDate = item.DueDate;
            itemFromDb.IsCompleted = item.IsCompleted;
            itemFromDb.IsAllDay = item.IsAllDay;

            if (itemFromDb.IsCompleted && itemFromDb.Frequency != RepeatFrequency.Never)
            {
                var nextItem = new ListItem()
                {
                    Title = itemFromDb.Title,
                    Description = itemFromDb.Description,
                    Frequency = itemFromDb.Frequency,
                    Importance = itemFromDb.Importance,
                    LastUpdatedDate = DateTime.Now,
                    DueDate = itemFromDb.DueDate.AddDays(AddDaysFromFrequency(itemFromDb.Frequency)),
                    IsCompleted = false,
                    IsAllDay = itemFromDb.IsAllDay,
                    ListTypeName= itemFromDb.ListTypeName,
                    Type = itemFromDb.Type,
                };
                unitOfWork.ListItem.Add(nextItem);
            }
            unitOfWork.Complete();
            return itemFromDb;
        }
        private static int AddDaysFromFrequency(RepeatFrequency frequency) =>
             frequency switch
             {
                 RepeatFrequency.Daily => 1,
                 RepeatFrequency.Weekly => 7,
                 RepeatFrequency.Monthly => 30,
                 RepeatFrequency.Quarterly => 90,
                 RepeatFrequency.Yearly => 365,
                 RepeatFrequency.Never => 0,
                 _ => 0,
             };
    }
}
