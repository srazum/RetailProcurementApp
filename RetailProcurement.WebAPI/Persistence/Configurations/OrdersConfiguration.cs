using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.OrderItemSuppliers)
                .WithOne(e => e.Order).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(e => e.Supplier)
                .WithMany(e => e.Orders).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
