namespace RetailProcurement.WebAPI.Entities
{
    public class StoreItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<SupplierStoreItem>? SupplierStoreItems { get; set; }
    }
}
