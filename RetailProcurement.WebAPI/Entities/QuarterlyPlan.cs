namespace RetailProcurement.WebAPI.Entities
{
    public class QuarterlyPlan
    {
        public int Id { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }
    }
}
