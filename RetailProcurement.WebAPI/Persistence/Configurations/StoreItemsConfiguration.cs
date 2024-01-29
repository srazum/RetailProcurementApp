using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class StoreItemsConfiguration : IEntityTypeConfiguration<StoreItem>
    {
        public void Configure(EntityTypeBuilder<StoreItem> builder)
        {
            builder.ToTable("StoreItems");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasMany(e => e.SupplierStoreItems)
                .WithOne(e => e.StoreItem)
                .HasForeignKey(e => e.StoreItemId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
