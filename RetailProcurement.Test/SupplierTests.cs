namespace RetailProcurement.UnitTests;

public class SupplierTests
{
    IGenericEntityOperations<Supplier> _supplierService;

    readonly string supplierName = "Test Supplier";
    readonly string supplierNewName = "Test Supplier 2";
    public SupplierTests()
    {

        var options = new DbContextOptionsBuilder<RetailProcurementDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
            .Options;
        var dbContext = new RetailProcurementDbContext(options);
        _supplierService = new GenericEntityOperations<Supplier>(dbContext);
    }

    [Fact]
    public void AddSupplierTest()
    {
        var result = _supplierService.GetAll();
        Assert.Empty(result);

        Supplier supplier = new()
        {
            Name = supplierName,
        };
        _supplierService.Insert(supplier);
        var id = supplier.Id;
        _supplierService.Save();
        result = _supplierService.GetAll();
        Assert.Single(result);
        _supplierService.Delete(id);
        _supplierService.Save();
        result = _supplierService.GetAll();
        Assert.Empty(result);
    }

    [Fact]
    public void UpdateSupplierTest()
    {
        var result = _supplierService.GetAll();
        Assert.Empty(result);

        Supplier supplier = new()
        {
            Name = supplierName,
        };
        _supplierService.Insert(supplier);
        var id = supplier.Id;
        supplier.Name = supplierNewName;
        _supplierService.Update(supplier);
        _supplierService.Save();
        var updatedSupplier = _supplierService.GetById(id);
        Assert.Equal(supplierNewName, updatedSupplier.Name);
        Assert.Single(result);
        _supplierService.Delete(id);
        _supplierService.Save();
    }
}