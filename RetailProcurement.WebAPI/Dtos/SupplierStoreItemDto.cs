using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Dtos
{
    public class SupplierStoreItemDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int StoreItemId { get; set; }
        public int SupplierId { get; set; }
    }
}
