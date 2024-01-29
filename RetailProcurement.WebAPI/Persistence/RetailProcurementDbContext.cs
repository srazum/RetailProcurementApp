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
        var someStoreItems = new Faker<StoreItem>()
            .RuleFor(e => e.Name, (f, e) => f.Commerce.Product())
            .RuleFor(e => e.Id, (f, e) => ++i);
        var storeItems = someStoreItems.Generate(10);
        modelBuilder.Entity<StoreItem>().HasData(storeItems);
        i = 0;
        var someSuppliersFaker = new Faker<Supplier>()
            .RuleFor(e => e.Name, (f, e) => f.Company.CompanyName())
            .RuleFor(e => e.Id, (f, e) => ++i);
        var suppliers = someSuppliersFaker.Generate(10);
        modelBuilder.Entity<Supplier>().HasData(suppliers);
        var pickedSuppliers = new List<int>();
        foreach (var supplier in suppliers)
        {
            i = 0;
            Faker<SupplierStoreItem> someSupplierStoreItems = new Faker<SupplierStoreItem>()
                    .StrictMode(false)
                    .UseSeed(5354)
                    .RuleFor(s => s.SupplierId, (f, e) => supplier.Id)
                    .RuleFor(s => s.StoreItemId, (f, e) => f.PickRandom(storeItems).Id)
                .RuleFor(e => e.Id, (f, e) => ++i);
            var supplierStoreItems = someSupplierStoreItems.Generate(10);
            modelBuilder.Entity<SupplierStoreItem>().HasData(supplierStoreItems);
            i = 0;
            Faker<Order> someOrders = new Faker<Order>()
                    .StrictMode(false)
                    .UseSeed(5354)
                    .RuleFor(s => s.SupplierId, (f, e) => supplier.Id)
                .RuleFor(e => e.Id, (f, e) => ++i);
            var orders = someOrders.Generate(10);
            modelBuilder.Entity<Order>().HasData(orders);
            i = 0;
            Faker<OrderItemSupplier> someOrderItemSuppliers = new Faker<OrderItemSupplier>()
                    .StrictMode(false)
                    .UseSeed(5354)
                    .RuleFor(s => s.SupplierStoreItemId, (f, e) => f.PickRandom(supplierStoreItems).Id)
                    .RuleFor(s => s.OrderId, (f, e) => f.PickRandom(orders).Id)
                .RuleFor(e => e.Id, (f, e) => ++i);
            var orderItemSuppliers = someOrderItemSuppliers.Generate(10);
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