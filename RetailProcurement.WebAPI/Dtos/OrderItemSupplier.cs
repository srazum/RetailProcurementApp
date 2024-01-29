namespace RetailProcurement.WebAPI.Dtos
{
    public class OrderItemSupplierDto
    {
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int SupplierStoreItemId { get; set; }
    }
}
