using Bogus;
using Microsoft.EntityFrameworkCore;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Persistence;

public class RetailProcurementDbContext : DbContext
{
    public RetailProcurementDbContext(DbContextOptions<RetailProcurementDbContext> options)
        : base(options)
    { }

    // onmodelcreating  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RetailProcurementDbContext).Assembly);
        int i = 0;
        var rand = new Random();
        var someStoreItems = new Faker<StoreItem>()
            .RuleFor(e => e.Name, (f, e) => f.Commerce.Product())
            .RuleFor(e => e.Id, (f, e) => ++i);
        var storeItems = someStoreItems.Generate(40);
        modelBuilder.Entity<StoreItem>().HasData(storeItems);
        i = 0;

        var pickedSuppliers = new List<int>();
        int supplierStoreItemNr = 0;
        int orderNr = 0;
        int orderItemSupplierNr = 0;
        int supplierNr = 0;
        int supplierMaxCount = 40;

        int itemPerSupplierMaxCount = 10;
        int supplierItemPerOrderMaxCount = 10;
        int orderNrMaxCount = 20;
        foreach (var supplierTempNr in Enumerable.Range(1, supplierMaxCount))
        {        
            var someSuppliersFaker = new Faker<Supplier>()
            .RuleFor(e => e.Name, (f, e) => f.Company.CompanyName())
            .RuleFor(e => e.Id, (f, e) => ++supplierNr);
            var f = new Bogus.Faker();
            var randomStoreItems = f.PickRandom(storeItems, itemPerSupplierMaxCount).ToList();
            int storeItemNr = 0;
            var supplier = someSuppliersFaker.Generate(1)[0];
            modelBuilder.Entity<Supplier>().HasData(supplier);
            Faker<SupplierStoreItem> someSupplierStoreItems = new Faker<SupplierStoreItem>()
                    .StrictMode(false)
                    .UseSeed(4534)
                    .RuleFor(s => s.SupplierId, (f, e) => supplier.Id)
                    .RuleFor(s => s.Price, (f, e) => Math.Round(((decimal)rand.NextDouble() * 3000), 2))
                    .RuleFor(s => s.StoreItemId, (f, e) => randomStoreItems[storeItemNr++].Id)
                .RuleFor(e => e.Id, (f, e) => ++supplierStoreItemNr);
            var supplierStoreItems = someSupplierStoreItems.Generate(itemPerSupplierMaxCount);
            modelBuilder.Entity<SupplierStoreItem>().HasData(supplierStoreItems);
            Faker<Order> someOrders = new Faker<Order>()
                    .StrictMode(false)
                    .UseSeed(5354)
                    .RuleFor(s => s.SupplierId, (f, e) => supplier.Id)
                .RuleFor(e => e.Id, (f, e) => ++orderNr);
            var orders = someOrders.Generate(rand.Next(1, orderNrMaxCount));
            modelBuilder.Entity<Order>().HasData(orders);
            int supplierStoreCounter = 0;
            var randomSupplierStoreItems = f.PickRandom(supplierStoreItems, supplierItemPerOrderMaxCount).ToList();
            Faker<OrderItemSupplier> someOrderItemSuppliers = new Faker<OrderItemSupplier>()
                    .StrictMode(false)
                    .UseSeed(5354)
                    .RuleFor(s => s.SupplierStoreItemId, (f, e) => randomSupplierStoreItems[supplierStoreCounter++].Id)
                    .RuleFor(s => s.Price, (f, e) => randomSupplierStoreItems[supplierStoreCounter].Price)
                    .RuleFor(s => s.Quantity, (f, e) => (decimal)rand.Next(1, 10))
                    .RuleFor(s => s.OrderId, (f, e) => f.PickRandom(orders).Id)
                .RuleFor(e => e.Id, (f, e) => ++orderItemSupplierNr);
            var orderItemSuppliers = someOrderItemSuppliers.Generate();
            modelBuilder.Entity<OrderItemSupplier>().HasData(orderItemSuppliers);
        }
    }

    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<StoreItem> StoreItems { get; set; }
    public DbSet<SupplierStoreItem> SupplierStoreItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItemSupplier> OrderItemSuppliers { get; set; }
    public DbSet<QuarterlyPlan> QuarterlyPlans { get; set; }
}