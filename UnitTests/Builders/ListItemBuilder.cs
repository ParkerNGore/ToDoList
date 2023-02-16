using Bogus;
using WebAPI.Models;
using WebAPI.Models.Dtos;

namespace UnitTests.Builders
{
    public static class ListItemBuilder
    {
        public static CreateListItemDto BuildRandom()
        {
            var faker = new Faker("en_US");

            return new CreateListItemDto()
            {
                Title = faker.Random.Word(),
                Description = faker.Random.Words(),
                DueDate = faker.Date.Soon(),
                Frequency = (RepeatFrequency)faker.Random.Number(0 , 5),
                Importance = (Importance)faker.Random.Number(0, 3),
                ListTypeName = "Default",
                IsNewListType = true,
            }; 
        }
    }
}
