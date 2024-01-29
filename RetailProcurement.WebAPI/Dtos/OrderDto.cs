using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public IEnumerable<StoreItemDto>? Items { get; set; }
    }
}
