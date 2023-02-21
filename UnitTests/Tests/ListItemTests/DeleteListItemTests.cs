using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using WebAPI.Services.ListItems;

namespace UnitTests.Tests.ListItemTests
{
    public class DeleteListItemTests
    {
        [Fact]
        public void ShouldDelete()
        {
            // Test feels messy, need to clean
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);

            var createListItemService = new CreateListItemService();

            var listType = ListTypeBuilder.BuildDefault();
            context.Add(listType);
            context.SaveChanges();

            var createListItemDto = ListItemBuilder.BuildRandom();
            var createdListItem = createListItemService.Create(createListItemDto);

            var listItemId = createdListItem.Id;

            Assert.NotNull(createdListItem);

            var deleteListItemService = new DeleteListItemService();
            deleteListItemService.Delete(createdListItem.Id);

            var nullListItem = context.ListItems.FirstOrDefault(x => x.Id == listItemId);
            var listTypeFromDb = context.ListTypes.Include(x => x.ListItems).FirstOrDefault(x => x.Name == listType.Id);

            Assert.Null(nullListItem);
            Assert.NotNull(listTypeFromDb);
            Assert.Null(listTypeFromDb.ListItems.FirstOrDefault(x => x.Id == listItemId));
        }
        [Fact]
        public void ShouldFailToDelete()
        {
            // Test feels somehow inadequate or not enough
            var context = ContextBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build(context);
            var deleteListItemService = new DeleteListItemService();

            Assert.Throws<Exception>(() => deleteListItemService.Delete("Not A GUID"));
        }
    }
}
