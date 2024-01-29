using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasMany(e => e.SupplierStoreItems)
                .WithOne(e => e.Supplier)
                .HasForeignKey(e => e.SupplierId).HasForeignKey(e => e.StoreItemId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
