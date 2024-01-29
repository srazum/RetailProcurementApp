using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence.Configurations
{
    public class QuarterlyPlanConfiguration : IEntityTypeConfiguration<QuarterlyPlan>
    {
        public void Configure(EntityTypeBuilder<QuarterlyPlan> builder)
        {
            builder.ToTable("QuarterlyPlans");
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Supplier)
                .WithMany(e => e.QuarterlyPlans).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
