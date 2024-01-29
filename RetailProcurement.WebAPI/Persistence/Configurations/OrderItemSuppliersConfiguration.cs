using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class OrderItemSuppliersConfiguration : IEntityTypeConfiguration<OrderItemSupplier>
    {
        public void Configure(EntityTypeBuilder<OrderItemSupplier> builder)
        {
            builder.ToTable("OrderItemSupplier");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Price)
                .IsRequired();
            builder.Property(e => e.Quantity)
                .IsRequired();
            builder.HasOne(e => e.SupplierStoreItem)
                .WithMany(e => e.OrderItemSuppliers).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}