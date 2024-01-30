namespace RetailProcurement.WebAPI.Dtos
{
    public class QuarterlyPlanDto
    {
        public int Id { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int SupplierId { get; set; }
    }
}
