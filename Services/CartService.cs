using Microsoft.EntityFrameworkCore;
using FashionMart.Data;
using FashionMart.Models;

namespace FashionMart.Services;

// Бизнес-сервис корзины покупателя
public interface ICartService
{
    Task<IEnumerable<CartItem>> GetCartAsync(int customerId);
    Task AddToCartAsync(int customerId, int productId, int quantity = 1);
    Task UpdateQuantityAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);
    Task ClearCartAsync(int customerId);
    Task<decimal> GetTotalAsync(int customerId);
}

public class CartService : ICartService
{
    private readonly FashionDbContext _wardrobe;

    public CartService(FashionDbContext context) => _wardrobe = context;

    // Содержимое корзины с подгрузкой товаров
    public async Task<IEnumerable<CartItem>> GetCartAsync(int customerId)
        => await _wardrobe.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.CustomerId == customerId)
            .ToListAsync();

    // Если товар уже в корзине — увеличиваем количество, иначе добавляем
    public async Task AddToCartAsync(int customerId, int productId, int quantity = 1)
    {
        var existing = await _wardrobe.CartItems
            .FirstOrDefaultAsync(ci => ci.CustomerId == customerId && ci.ProductId == productId);

        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            await _wardrobe.CartItems.AddAsync(new CartItem
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            });
        }
        await _wardrobe.SaveChangesAsync();
    }

    // Количество <= 0 удаляет позицию из корзины
    public async Task UpdateQuantityAsync(int cartItemId, int quantity)
    {
        var item = await _wardrobe.CartItems.FindAsync(cartItemId);
        if (item is null) return;

        if (quantity <= 0)
            _wardrobe.CartItems.Remove(item);
        else
            item.Quantity = quantity;

        await _wardrobe.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var item = await _wardrobe.CartItems.FindAsync(cartItemId);
        if (item is null) return;
        _wardrobe.CartItems.Remove(item);
        await _wardrobe.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int customerId)
    {
        var items = _wardrobe.CartItems.Where(ci => ci.CustomerId == customerId);
        _wardrobe.CartItems.RemoveRange(items);
        await _wardrobe.SaveChangesAsync();
    }

    // Общая стоимость корзины
    public async Task<decimal> GetTotalAsync(int customerId)
        => await _wardrobe.CartItems
            .Where(ci => ci.CustomerId == customerId)
            .SumAsync(ci => ci.Quantity * ci.Product.Price);
}
