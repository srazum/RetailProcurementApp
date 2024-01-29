namespace RetailProcurement.UnitTests;

public class StoreItemTests
{
    IGenericEntityOperations<StoreItem> _storeItemService;

    readonly string storeItemName = "Test Store name";
    readonly string storeItemNewName = "Test Store name 2";
    public StoreItemTests()
    {

        var options = new DbContextOptionsBuilder<RetailProcurementDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
            .Options;
        var dbContext = new RetailProcurementDbContext(options);
        _storeItemService = new GenericEntityOperations<StoreItem>(dbContext);
    }

    [Fact]
    public void AddStoreItemTest()
    {
        var result = _storeItemService.GetAll();
        Assert.Empty(result);
        var storeItem = new StoreItem()
        {
            Name = storeItemName,
        };
        _storeItemService.Insert(storeItem);
        var id = storeItem.Id;
        _storeItemService.Save();
        result = _storeItemService.GetAll();
        Assert.Single(result);
        _storeItemService.Delete(id);
        _storeItemService.Save();
        result = _storeItemService.GetAll();
        Assert.Empty(result);
    }

    [Fact]
    public void UpdateSupplierTest()
    {
        var result = _storeItemService.GetAll();
        Assert.Empty(result);
        var storeItem = new StoreItem()
        {
            Name = storeItemName,
        };
        _storeItemService.Insert(storeItem);
        var id = storeItem.Id;
        storeItem.Name = storeItemNewName;
        _storeItemService.Update(storeItem);
        _storeItemService.Save();
        var updatedStoreItem = _storeItemService.GetById(id);
        Assert.Equal(storeItemNewName, updatedStoreItem.Name);
        Assert.Single(result);
        _storeItemService.Delete(id);
        _storeItemService.Save();
    }
}