using Microsoft.EntityFrameworkCore;
using FashionMart.Data;
using FashionMart.Models;

namespace FashionMart.Repositories;

// Расширенный репозиторий товаров с подгрузкой связанных данных
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllWithCategoryAsync();
    Task<Product?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(FashionDbContext context) : base(context) { }

    // Список товаров с категориями (без отслеживания — read-only сценарий)
    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        => await _wardrobe.Products.Include(p => p.Category).AsNoTracking().ToListAsync();

    // Один товар со всеми связанными сущностями
    public async Task<Product?> GetByIdWithDetailsAsync(int id)
        => await _wardrobe.Products
            .Include(p => p.Category)
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == id);

    // Фильтрация по категории
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        => await _wardrobe.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync();
}
