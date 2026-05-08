using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using FashionMart.Data;

namespace FashionMart.Repositories;

// Базовая реализация универсального репозитория поверх EF Core
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly FashionDbContext _wardrobe;
    protected readonly DbSet<T> _entries;

    public Repository(FashionDbContext context)
    {
        _wardrobe = context;
        _entries = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() => await _entries.ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) => await _entries.FindAsync(id);

    // Поиск по произвольному условию
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _entries.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _entries.AddAsync(entity);
    public void Update(T entity) => _entries.Update(entity);
    public void Remove(T entity) => _entries.Remove(entity);

    public async Task<int> SaveChangesAsync() => await _wardrobe.SaveChangesAsync();
}
