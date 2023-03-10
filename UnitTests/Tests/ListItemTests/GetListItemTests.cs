using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using WebAPI.Services.ListItems;

namespace UnitTests.Tests.ListItemTests
{
    public class GetListItemTests
    {
        [Fact]
        public void ShouldGetSingle()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var getListItemService = new GetListService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var itemFromDb = getListItemService.GetListItem(createdListItem.Id);

            Assert.NotNull(itemFromDb);

            Assert.Equal(createdListItem.Title, itemFromDb.Title);
            Assert.Equal(createdListItem.Description, itemFromDb.Description);
            Assert.Equal(createdListItem.Importance, itemFromDb.Importance);
            Assert.Equal(createdListItem.Frequency, itemFromDb.Frequency);
            Assert.Equal(createdListItem.ListTypeName, itemFromDb.ListTypeName);
            Assert.Equal(createdListItem.CreatedDate, itemFromDb.CreatedDate);
            Assert.Equal(createdListItem.LastUpdatedDate, itemFromDb.LastUpdatedDate);
            Assert.Equal(createdListItem.DueDate, itemFromDb.DueDate);
        }
        [Fact]
        public void ShouldGetAllByType()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var getAllListsByTypeService = new FilterListItemsService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);
            createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);
            createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);

            var list = getAllListsByTypeService.FilterListItems("Default", false);

            Assert.True(list.Any());
            Assert.True(list.Count() == 3);
        }
        [Fact]
        public void ShouldGetAllByTypeNotCompleted()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var filterListByType = new FilterListItemsService(unitOfWork);

            var itemType = ListTypeBuilder.BuildDefault();
            context.Add(itemType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);
            createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);
            createListItemDto = ListItemBuilder.BuildRandom();
            createListItemService.Create(createListItemDto);

            var listFromDb = context.ListItems.ToList();
            listFromDb.First().IsCompleted = true;
            context.SaveChanges();


            var list = filterListByType.FilterListItems("Default", true);

            Assert.NotNull(listFromDb);
            Assert.NotNull(list);

            Assert.True(listFromDb.Any());
            Assert.True(list.Any());

            Assert.True(list.Count() == 2);
            Console.WriteLine(listFromDb.Count());
            Assert.True(listFromDb.Count() == 3);

        }
        [Fact]
        public void ShouldFailGetSingle()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var getListItemService = new GetListService(unitOfWork);

            context.Add(ListTypeBuilder.BuildDefault());
            context.SaveChanges();

            createListItemService.Create(ListItemBuilder.BuildRandom());

            Assert.Throws<Exception>(() => getListItemService.GetListItem(new Guid().ToString()));
        }
        [Fact]
        public void ShouldGetAll()
        {
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService(unitOfWork);
            var getAllListsService = new GetAllListsService(unitOfWork);

            context.Add(ListTypeBuilder.BuildDefault());
            var newType = ListTypeBuilder.BuildDefault();
            newType.Name = "NewType";
            context.Add(newType);
            context.SaveChanges();

            var firstItem = ListItemBuilder.BuildRandom();
            var firstItemCreated = createListItemService.Create(firstItem);
            var secondItem = ListItemBuilder.BuildRandom();
            secondItem.ListTypeName = "NewType";
            var secondItemCreated = createListItemService.Create(secondItem);

            var list = getAllListsService.GetAllLists();

            Assert.True(list.Any());
            Assert.True(list.Count() == 2);

            var firstItemFromDb = list.FirstOrDefault(x => x.ListTypeName == "Default");
            var secondItemFromDb = list.FirstOrDefault(x => x.ListTypeName == "NewType");

            Assert.Equal(firstItemCreated.Title, firstItemFromDb.Title);
            Assert.Equal(firstItemCreated.Description, firstItemFromDb.Description);
            Assert.Equal(firstItemCreated.Importance, firstItemFromDb.Importance);
            Assert.Equal(firstItemCreated.Frequency, firstItemFromDb.Frequency);
            Assert.Equal(firstItemCreated.ListTypeName, firstItemFromDb.ListTypeName);
            Assert.Equal(firstItemCreated.CreatedDate, firstItemFromDb.CreatedDate);
            Assert.Equal(firstItemCreated.LastUpdatedDate, firstItemFromDb.LastUpdatedDate);
            Assert.Equal(firstItemCreated.DueDate, firstItemFromDb.DueDate);

            Assert.Equal(secondItemCreated.Title, secondItemFromDb.Title);
            Assert.Equal(secondItemCreated.Description, secondItemFromDb.Description);
            Assert.Equal(secondItemCreated.Importance, secondItemFromDb.Importance);
            Assert.Equal(secondItemCreated.Frequency, secondItemFromDb.Frequency);
            Assert.Equal(secondItemCreated.ListTypeName, secondItemFromDb.ListTypeName);
            Assert.Equal(secondItemCreated.CreatedDate, secondItemFromDb.CreatedDate);
            Assert.Equal(secondItemCreated.LastUpdatedDate, secondItemFromDb.LastUpdatedDate);
            Assert.Equal(secondItemCreated.DueDate, secondItemFromDb.DueDate);
        }
    }
}
