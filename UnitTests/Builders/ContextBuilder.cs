using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI.Data;

namespace UnitTests.Builders
{
    public static class ContextBuilder
    {
        public static DataContext Build()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var context = new DataContext(options);

            return context;
        }
    }
}
