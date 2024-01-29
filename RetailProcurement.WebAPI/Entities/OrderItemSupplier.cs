namespace RetailProcurement.WebAPI.Entities
{
    public class OrderItemSupplier
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public int SupplierStoreItemId { get; set; }
        public virtual SupplierStoreItem? SupplierStoreItem { get; set; }
    }
}
