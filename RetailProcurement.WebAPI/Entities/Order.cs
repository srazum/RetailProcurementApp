namespace RetailProcurement.WebAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderItemSupplier>? OrderItemSuppliers { get; set; }
    }
}
