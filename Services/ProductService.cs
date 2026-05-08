using FashionMart.Models;
using FashionMart.Repositories;

namespace FashionMart.Services;

// Бизнес-сервис для работы с товарами
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _outfitRepo;

    public ProductService(IProductRepository repo) => _outfitRepo = repo;

    public Task<IEnumerable<Product>> GetAllAsync() => _outfitRepo.GetAllWithCategoryAsync();
    public Task<Product?> GetByIdAsync(int id) => _outfitRepo.GetByIdWithDetailsAsync(id);
    public Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId) => _outfitRepo.GetByCategoryAsync(categoryId);

    public async Task CreateAsync(Product product)
    {
        await _outfitRepo.AddAsync(product);
        await _outfitRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _outfitRepo.Update(product);
        await _outfitRepo.SaveChangesAsync();
    }

    // Удаление: сначала ищем сущность, потом удаляем
    public async Task DeleteAsync(int id)
    {
        var p = await _outfitRepo.GetByIdAsync(id);
        if (p is null) return;
        _outfitRepo.Remove(p);
        await _outfitRepo.SaveChangesAsync();
    }
}
