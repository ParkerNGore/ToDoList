using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using WebAPI.Models.DbModels;
using WebAPI.Services.ListItems;

namespace UnitTests.Tests.ListItemTests
{
    public class CreateListItemTests
    {
        [Fact]
        public void ShouldCreate()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);


            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            createListItemDto.IsNewListType = false;
            var createdListItem = createListItemService.Create(createListItemDto);

            var listItemFromDb = context.ListItems.FirstOrDefault(x => x.Id == createdListItem.Id);

            Assert.NotNull(listItemFromDb);

            Assert.Equal(createdListItem.Title, listItemFromDb.Title);
            Assert.Equal(createdListItem.Description, listItemFromDb.Description);
            Assert.Equal(createdListItem.Importance, listItemFromDb.Importance);
            Assert.Equal(createdListItem.Frequency, listItemFromDb.Frequency);
            Assert.Equal(createdListItem.ListTypeName, listItemFromDb.ListTypeName);
            Assert.Equal(createdListItem.CreatedDate, listItemFromDb.CreatedDate);
            Assert.Equal(createdListItem.LastUpdatedDate, listItemFromDb.LastUpdatedDate);
            Assert.Equal(createdListItem.DueDate, listItemFromDb.DueDate);

            Assert.Equal(createListItemDto.Title, listItemFromDb.Title);
            Assert.Equal(createListItemDto.Description, listItemFromDb.Description);
            Assert.Equal(createListItemDto.Importance, listItemFromDb.Importance);
            Assert.Equal(createListItemDto.Frequency, listItemFromDb.Frequency);
            Assert.Equal(createListItemDto.ListTypeName, listItemFromDb.ListTypeName);
            Assert.Equal(createListItemDto.DueDate, listItemFromDb.DueDate);
        }

        [Fact]
        public void ShouldFailToCreateWithBadType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);

            var createListItemDto = ListItemBuilder.BuildRandom();
            createListItemDto.IsNewListType = false;

            Assert.Throws<Exception>(() => createListItemService.Create(createListItemDto));
        }

        [Fact]
        public void ShouldFailToCreateWithDuplicateType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();

            Assert.Throws<Exception>(() => createListItemService.Create(createListItemDto));
        }

        [Fact]
        public void ShouldCreateWithType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);

            var createListItemDto = ListItemBuilder.BuildRandom();
            createListItemDto.ListTypeName = "NewType";

            ListType? typeFromDb = context.ListTypes.FirstOrDefault(x => x.Name == "NewType");
            Assert.Null(typeFromDb);

            var createdListItem = createListItemService.Create(createListItemDto);

            var listItemFromDb = context.ListItems.FirstOrDefault(x => x.Id == createdListItem.Id);

            typeFromDb = context.ListTypes.FirstOrDefault(x => x.Name == "NewType");

            Assert.NotNull(typeFromDb);
            Assert.NotNull(listItemFromDb);

            Assert.Equal(createdListItem.Title, listItemFromDb.Title);
            Assert.Equal(createdListItem.Description, listItemFromDb.Description);
            Assert.Equal(createdListItem.Importance, listItemFromDb.Importance);
            Assert.Equal(createdListItem.Frequency, listItemFromDb.Frequency);
            Assert.Equal(createdListItem.ListTypeName, listItemFromDb.ListTypeName);
            Assert.Equal(createdListItem.CreatedDate, listItemFromDb.CreatedDate);
            Assert.Equal(createdListItem.LastUpdatedDate, listItemFromDb.LastUpdatedDate);
            Assert.Equal(createdListItem.DueDate, listItemFromDb.DueDate);

            Assert.Equal(createListItemDto.Title, listItemFromDb.Title);
            Assert.Equal(createListItemDto.Description, listItemFromDb.Description);
            Assert.Equal(createListItemDto.Importance, listItemFromDb.Importance);
            Assert.Equal(createListItemDto.Frequency, listItemFromDb.Frequency);
            Assert.Equal(createListItemDto.ListTypeName, listItemFromDb.ListTypeName);
            Assert.Equal(createListItemDto.DueDate, listItemFromDb.DueDate);
        }


    }
}
