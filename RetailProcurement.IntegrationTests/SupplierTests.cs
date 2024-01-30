namespace RetailProcurement.IntegrationTests;

public class SupplierTests
{
    IGenericEntityOperations<Supplier> _supplierService;

    readonly string supplierName = "Test Supplier";
    readonly string supplierNewName = "Test Supplier 2";
    public SupplierTests()
    {
        var dbContext = DbContextProvider.CreateDbContext();
        _supplierService = new GenericEntityOperations<Supplier>(dbContext);
    }

    [Fact]
    public void AddSupplierTest()
    {
        Supplier supplier = new()
        {
            Name = supplierName,
        };
        _supplierService.Insert(supplier);
        _supplierService.Save();
        var id = supplier.Id;
        var result = _supplierService.GetAll();
        _supplierService.Delete(id);
        _supplierService.Save();
        var result2 = _supplierService.GetById(id);
        Assert.Null(result2);
    }

    [Fact]
    public void UpdateSupplierTest()
    {
        Supplier supplier = new()
        {
            Name = supplierName,
        };
        _supplierService.Insert(supplier);
        _supplierService.Save();
        var id = supplier.Id;
        supplier.Name = supplierNewName;
        _supplierService.Update(supplier);
        _supplierService.Save();
        var updatedSupplier = _supplierService.GetById(id);
        Assert.Equal(supplierNewName, updatedSupplier.Name);
        _supplierService.Delete(id);
        _supplierService.Save();
        var result2 = _supplierService.GetById(id);
        Assert.Null(result2);
    }
}