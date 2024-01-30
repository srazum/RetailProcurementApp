namespace RetailProcurement.IntegrationTests;

public class StoreItemTests
{
    IGenericEntityOperations<StoreItem> _storeItemService;

    readonly string storeItemName = "Test Store name";
    readonly string storeItemNewName = "Test Store name 2";
    public StoreItemTests()
    {
        var dbContext = DbContextProvider.CreateDbContext();
        _storeItemService = new GenericEntityOperations<StoreItem>(dbContext);
    }

    [Fact]
    public void AddStoreItemTest()
    {
        var storeItem = new StoreItem()
        {
            Name = storeItemName,
        };
        _storeItemService.Insert(storeItem);
        _storeItemService.Save();
        var id = storeItem.Id;
        var result = _storeItemService.GetAll();
        _storeItemService.Delete(id);
        _storeItemService.Save();

        var result2 = _storeItemService.GetById(id);
        Assert.Null(result2);
    }

    [Fact]
    public void UpdateSupplierTest()
    {
        var storeItem = new StoreItem()
        {
            Name = storeItemName,
        };
        _storeItemService.Insert(storeItem);
        _storeItemService.Save();
        var id = storeItem.Id;
        storeItem.Name = storeItemNewName;
        _storeItemService.Update(storeItem);
        _storeItemService.Save();
        var updatedStoreItem = _storeItemService.GetById(id);
        Assert.Equal(storeItemNewName, updatedStoreItem.Name);
        _storeItemService.Delete(id);
        _storeItemService.Save();
        var result2 = _storeItemService.GetById(id);
        Assert.Null(result2);
    }
}