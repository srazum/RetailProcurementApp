using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class SupplierStoreItemsConfiguration : IEntityTypeConfiguration<SupplierStoreItem>
    {
        public void Configure(EntityTypeBuilder<SupplierStoreItem> builder)
        {
            builder.ToTable("SupplierStoreItems");
            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(e => new { e.StoreItemId, e.SupplierId });
            builder.HasOne(e => e.StoreItem)
                .WithMany(e => e.SupplierStoreItems)
                .HasForeignKey(e => e.StoreItemId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(e => e.Supplier)
                .WithMany(e => e.SupplierStoreItems)
                .HasForeignKey(e => e.SupplierId).HasForeignKey(e => e.StoreItemId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
