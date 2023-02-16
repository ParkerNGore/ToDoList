using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.DbModels;

namespace WebAPI.Data.Configurations
{
    public class ListItemConfiguration : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Frequency).HasColumnType("nvarchar(24)");
            builder.Property(x => x.Importance).HasColumnType("nvarchar(24)");

            builder
                .HasOne(x => x.Type)
                .WithMany(x => x.ListItems)
                .HasForeignKey(x => x.ListTypeName)
                .HasPrincipalKey(x => x.Name)
                .IsRequired();
        }
    }
}
