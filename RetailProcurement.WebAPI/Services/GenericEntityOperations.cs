using Microsoft.EntityFrameworkCore;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Services;

public sealed class GenericEntityOperations<T> : IGenericEntityOperations<T> where T : class
{

    private readonly RetailProcurementDbContext _context;
    private readonly DbSet<T> _table;

    public GenericEntityOperations(RetailProcurementDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _table.ToList();
    }

    public T GetById(object id)
    {
        return _table.Find(id)!;
    }

    public void Insert(T obj)
    {
        _table.Add(obj);
    }

    public void Update(T obj)
    {
        _table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        T? existing = _table.Find(id);
        _table.Remove(existing!);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}