using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using WebAPI.Models;
using WebAPI.Services.ListItems;

namespace UnitTests.Tests.ListItemTests
{
    public class UpdateListItemTests
    {
        [Fact]
        public void ShouldUpdate()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService();

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var originalItem = context.ListItems.AsNoTracking().FirstOrDefault(x => x.Id == createdListItem.Id);

            DateTime savedDate = DateTime.Now;
            Importance newImportance = createdListItem.Importance != Importance.Critical ? Importance.Critical : Importance.Low;
            RepeatFrequency newFrequency = createdListItem.Frequency != RepeatFrequency.Daily ? RepeatFrequency.Daily : RepeatFrequency.Weekly;

            createdListItem.CreatedDate = savedDate;
            createdListItem.DueDate = DateTime.Now.AddDays(7);
            createdListItem.Description = "New Description";
            createdListItem.Importance = newImportance;
            createdListItem.Frequency = newFrequency;
            createdListItem.Title = "New Title";
            createdListItem.LastUpdatedDate = savedDate;

            var updatedList = updateListItemService.UpdateListItem(createdListItem);

            Assert.NotNull(updatedList);

            Assert.NotEqual(updatedList.CreatedDate, savedDate);
            Assert.Equal(updatedList.CreatedDate, originalItem.CreatedDate);

            Assert.NotEqual(updatedList.LastUpdatedDate, originalItem.LastUpdatedDate);
            Assert.NotEqual(updatedList.LastUpdatedDate, savedDate);
            Assert.True(updatedList.LastUpdatedDate > savedDate);

            Assert.NotEqual(updatedList.Description, originalItem.Description);
            Assert.Equal("New Description", updatedList.Description);

            Assert.NotEqual(updatedList.Title, originalItem.Title);
            Assert.Equal("New Title", updatedList.Title);

            Assert.NotEqual(updatedList.Importance, originalItem.Importance);
            Assert.Equal(newImportance, updatedList.Importance);

            Assert.NotEqual(updatedList.Frequency, originalItem.Frequency);
            Assert.Equal(newFrequency, updatedList.Frequency);

            Assert.Equal(updatedList.ListTypeName, originalItem.ListTypeName);
        }

        [Fact]
        public void ShouldFailUpdateListType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService();

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            createdListItem.ListTypeName = "Non-Existent ListType";

            Assert.Throws<Exception>(() => updateListItemService.UpdateListItem(createdListItem));
        }
        [Fact]
        public void ShouldChangeListType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService();

            var itemType = ListTypeBuilder.BuildDefault();

            var newItemType = ListTypeBuilder.BuildDefault();
            newItemType.Name = "NewType";

            context.Add(itemType);
            context.Add(newItemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);
            
            var listItem = context.ListItems.Include(x => x.Type).FirstOrDefault(x => x.Id == createdListItem.Id);

            Assert.NotNull(listItem);

            Assert.Equal(itemType.Name, listItem.ListTypeName);
            Assert.NotNull(itemType.ListItems.FirstOrDefault(x => x.Id == listItem.Id));

            listItem.Type = newItemType;
            context.SaveChanges();

            Assert.Null(itemType.ListItems.FirstOrDefault(x => x.Id == listItem.Id));

            Assert.Equal(newItemType.Name, listItem.ListTypeName);
            Assert.NotNull(newItemType.ListItems.FirstOrDefault(x => x.Id == listItem.Id));
        }
    }
}
