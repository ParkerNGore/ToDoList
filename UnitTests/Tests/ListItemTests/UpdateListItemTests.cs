using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var updateListItemService = new UpdateListItemService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var itemToUpdate = context.ListItems.AsNoTracking().FirstOrDefault(x => x.Id == createdListItem.Id);
            var itemFromDb = context.ListItems.AsNoTracking().FirstOrDefault(x => x.Id == createdListItem.Id);

            DateTime savedDate = DateTime.Now;
            Importance newImportance = createdListItem.Importance != Importance.Critical ? Importance.Critical : Importance.Low;
            RepeatFrequency newFrequency = createdListItem.Frequency != RepeatFrequency.Daily ? RepeatFrequency.Daily : RepeatFrequency.Weekly;

            itemToUpdate.CreatedDate = savedDate;
            itemToUpdate.DueDate = DateTime.Now.AddDays(7);
            itemToUpdate.Description = "New Description";
            itemToUpdate.Importance = newImportance;
            itemToUpdate.Frequency = newFrequency;
            itemToUpdate.Title = "New Title";
            itemToUpdate.LastUpdatedDate = savedDate;

            var updatedList = updateListItemService.UpdateListItem(itemToUpdate, false);

            Assert.NotNull(updatedList);

            Assert.NotEqual(updatedList.CreatedDate, savedDate);
            Assert.Equal(updatedList.CreatedDate, itemFromDb.CreatedDate);

            Assert.NotEqual(updatedList.LastUpdatedDate, itemFromDb.LastUpdatedDate);
            Assert.True(updatedList.LastUpdatedDate > itemFromDb.CreatedDate);

            Assert.NotEqual(updatedList.DueDate, itemFromDb.DueDate);
            Assert.True(updatedList.DueDate > itemFromDb.DueDate);

            Assert.NotEqual(updatedList.Description, itemFromDb.Description);
            Assert.Equal("New Description", updatedList.Description);

            Assert.NotEqual(updatedList.Title, itemFromDb.Title);
            Assert.Equal("New Title", updatedList.Title);

            Assert.NotEqual(updatedList.Importance, itemFromDb.Importance);
            Assert.Equal(newImportance, updatedList.Importance);

            Assert.NotEqual(updatedList.Frequency, itemFromDb.Frequency);
            Assert.Equal(newFrequency, updatedList.Frequency);

            Assert.Equal(updatedList.ListTypeName, itemFromDb.ListTypeName);
        }

        [Fact]
        public void ShouldFailUpdateListType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var updatedList = context.ListItems.AsNoTracking().FirstOrDefault(x => x.Id == createdListItem.Id);
            updatedList.ListTypeName = "Non-Existent List Type";

            Assert.Throws<Exception>(() => updateListItemService.UpdateListItem(updatedList, false));
        }
        [Fact]
        public void ShouldChangeListType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService(unitOfWork);

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
        [Fact]
        public void ShouldChangeListTypeWithNewListType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var updateListItemService = new UpdateListItemService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();

            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var newItemType = ListTypeBuilder.BuildDefault();
            newItemType.Name = "NewType";

            var itemFromDb = context.ListItems.AsNoTracking().Include(x => x.Type).FirstOrDefault(x => x.Id == createdListItem.Id);
            itemFromDb.ListTypeName = newItemType.Name;
            itemFromDb.Type = newItemType;

            var updatedList = updateListItemService.UpdateListItem(itemFromDb, true);

            var listTypeFromDb = context.ListTypes.Include(x => x.ListItems).FirstOrDefault(x => x.Name == newItemType.Name);

            var listItem = context.ListItems.Include(x => x.Type).FirstOrDefault(x => x.Id == createdListItem.Id);

            Assert.NotNull(listItem);

            Assert.Null(itemType.ListItems.FirstOrDefault(x => x.Id == listItem.Id));

            Assert.Equal(newItemType.Name, listItem.ListTypeName);
            Assert.NotNull(listTypeFromDb.ListItems.FirstOrDefault(x => x.Id == listItem.Id));
        }
    }
}
