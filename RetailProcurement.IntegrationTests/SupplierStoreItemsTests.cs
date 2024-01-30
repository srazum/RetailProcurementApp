namespace RetailProcurement.IntegrationTests;

public class SupplierStoreItemTests
{
    IGenericEntityOperations<SupplierStoreItem> _supplierStoreItemService;
    private GenericEntityOperations<Supplier> _supplierService;
    private GenericEntityOperations<StoreItem> _storeItemService;
    readonly string storeItemName = "Test Store name";
    readonly string storeItemNewName = "Test Store name 2";
    readonly string supplierName = "Test Supplier";
    readonly string supplierNewName = "Test Supplier 2";
    public SupplierStoreItemTests()
    {
        var dbContext = DbContextProvider.CreateDbContext();
        _supplierStoreItemService = new GenericEntityOperations<SupplierStoreItem>(dbContext);
        _supplierService = new GenericEntityOperations<Supplier>(dbContext);
        _storeItemService = new GenericEntityOperations<StoreItem>(dbContext);
    }

    [Fact]
    public void AddGetUpdateDeleteSupplierStoreItemTest()
    {
        var result = _supplierStoreItemService.GetAll();
        Assert.Empty(result);
        var storeItem = new StoreItem()
        {
            Name = storeItemName,
        };
        _storeItemService.Insert(storeItem);
        _storeItemService.Save();
        Supplier supplier = new()
        {
            Name = supplierName,
        };
        _supplierService.Insert(supplier);
        _supplierService.Save();
        var supplierStoreItem = new SupplierStoreItem()
        {
            StoreItemId = storeItem.Id,
            SupplierId = supplier.Id,
        };
        _supplierStoreItemService.Insert(supplierStoreItem);
        _supplierStoreItemService.Save();
        var id = supplierStoreItem.Id;
        result = _supplierStoreItemService.GetAll();
        Assert.Single(result);
        _supplierStoreItemService.Delete(id);
        _supplierStoreItemService.Save();
        result = _supplierStoreItemService.GetAll();
        Assert.Empty(result);
    }

}