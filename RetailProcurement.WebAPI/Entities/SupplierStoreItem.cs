namespace RetailProcurement.WebAPI.Entities
{
    public class SupplierStoreItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int StoreItemId { get; set; }
        public StoreItem? StoreItem { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public virtual ICollection<OrderItemSupplier>? OrderItemSuppliers { get; set; }
    }
}
