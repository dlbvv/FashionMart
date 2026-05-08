using FashionMart.Models;
using FashionMart.Repositories;

namespace FashionMart.Services;

// Бизнес-сервис для работы с категориями товаров
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task CreateAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
}

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _outfitRepo;

    public CategoryService(IRepository<Category> repo) => _outfitRepo = repo;

    public Task<IEnumerable<Category>> GetAllAsync() => _outfitRepo.GetAllAsync();
    public Task<Category?> GetByIdAsync(int id) => _outfitRepo.GetByIdAsync(id);

    public async Task CreateAsync(Category category)
    {
        await _outfitRepo.AddAsync(category);
        await _outfitRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _outfitRepo.Update(category);
        await _outfitRepo.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var c = await _outfitRepo.GetByIdAsync(id);
        if (c is null) return;
        _outfitRepo.Remove(c);
        await _outfitRepo.SaveChangesAsync();
    }
}
