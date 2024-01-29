namespace RetailProcurement.UnitTests;

public class SupplierStoreItemTests
{
    IGenericEntityOperations<SupplierStoreItem> _supplierStoreItemService;
    public SupplierStoreItemTests()
    {

        var options = new DbContextOptionsBuilder<RetailProcurementDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
            .Options;
        var dbContext = new RetailProcurementDbContext(options);
        _supplierStoreItemService = new GenericEntityOperations<SupplierStoreItem>(dbContext);
    }

    [Fact]
    public void AddSupplierStoreItemTest()
    {
        var result = _supplierStoreItemService.GetAll();
        Assert.Empty(result);
        var supplierStoreItem = new SupplierStoreItem()
        {
            //Name = "Test SupplierStoreItem",
        };
        _supplierStoreItemService.Insert(supplierStoreItem);
        var id = supplierStoreItem.Id;
        _supplierStoreItemService.Save();
        result = _supplierStoreItemService.GetAll();
        Assert.Single(result);
        _supplierStoreItemService.Delete(id);
        _supplierStoreItemService.Save();
        result = _supplierStoreItemService.GetAll();
        Assert.Empty(result);
    }

    [Fact]
    public void UpdateSupplierStoreItemTest()
    {
        var result = _supplierStoreItemService.GetAll();
        Assert.Empty(result);
        var supplierStoreItem = new SupplierStoreItem()
        {
            //Name = "Test SupplierStoreItem",
        };
        _supplierStoreItemService.Insert(supplierStoreItem);
        var id = supplierStoreItem.Id;
        //supplierStoreItem.Name = "Test SupplierStoreItem 2";
        _supplierStoreItemService.Update(supplierStoreItem);
        _supplierStoreItemService.Save();
        var updatedSupplierStoreItem = _supplierStoreItemService.GetById(id);
        //Assert.Equal("Test SupplierStoreItem 2", updatedSupplierStoreItem.Name);
        Assert.Single(result);
        _supplierStoreItemService.Delete(id);
        _supplierStoreItemService.Save();
    }
}