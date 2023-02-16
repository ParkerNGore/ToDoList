using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.DbModels;

namespace WebAPI.Data.Configurations
{
    public class ListTypeConfiguration : IEntityTypeConfiguration<ListType>
    {
        public void Configure(EntityTypeBuilder<ListType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Name);
        }
    }
}
