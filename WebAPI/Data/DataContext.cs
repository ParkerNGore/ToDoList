using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Configurations;
using WebAPI.Models.DbModels;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<ListType> ListTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ListItemConfiguration());
            builder.ApplyConfiguration(new ListTypeConfiguration());
        }
    }
}
