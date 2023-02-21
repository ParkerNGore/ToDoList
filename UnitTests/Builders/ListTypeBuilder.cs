using Bogus;
using WebAPI.Models.DbModels;

namespace UnitTests.Builders
{
    public static class ListTypeBuilder
    {
        public static ListType BuildDefault() => new() { Description = "Default Description", Name = "Default" };

        public static ListType BuildRandom()
        {
            var faker = new Faker("en_US");

            return new()
            {
                Description = faker.Random.Words(),
                Name = faker.Random.Word(),
            };
        }
    }
}
