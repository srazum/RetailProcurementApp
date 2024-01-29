namespace RetailProcurement.WebAPI.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<SupplierStoreItem>? SupplierStoreItems { get; set; }
        public virtual ICollection<OrderItemSupplier>? OrderItemSuppliers { get; set; }
        public virtual ICollection<QuarterlyPlan>? QuarterlyPlans { get; set; }
    }
}
