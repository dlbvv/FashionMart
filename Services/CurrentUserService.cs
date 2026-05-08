using Microsoft.EntityFrameworkCore;
using FashionMart.Data;
using FashionMart.Models;

namespace FashionMart.Services;

// Упрощённый сервис текущего пользователя (без авторизации, для демо)
public interface ICurrentUserService
{
    Task<Customer> GetOrCreateGuestShopperAsync();
}

public class CurrentUserService : ICurrentUserService
{
    private readonly FashionDbContext _wardrobe;

    // Email фиктивного покупателя демо-режима
    private const string GuestShopperEmail = "guest@fashionmart.local";

    public CurrentUserService(FashionDbContext context) => _wardrobe = context;

    // Возвращает существующего демо-покупателя или создаёт нового
    public async Task<Customer> GetOrCreateGuestShopperAsync()
    {
        var customer = await _wardrobe.Customers.FirstOrDefaultAsync(c => c.Email == GuestShopperEmail);

        if (customer == null)
        {
            customer = new Customer
            {
                FullName = "Demo Shopper",
                Email = GuestShopperEmail,
                RegisteredAt = DateTime.UtcNow
            };
            _wardrobe.Customers.Add(customer);
            await _wardrobe.SaveChangesAsync();
        }
        return customer;
    }
}
