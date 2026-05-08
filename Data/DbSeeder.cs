using Microsoft.EntityFrameworkCore;
using FashionMart.Models;

namespace FashionMart.Data;

// Начальные данные модного магазина FashionMart
public static class DbSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Платья", Description = "Вечерние и повседневные платья" },
            new Category { Id = 2, Name = "Обувь", Description = "Туфли, ботинки, кроссовки" },
            new Category { Id = 3, Name = "Сумки", Description = "Сумки, рюкзаки, кошельки" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Платье Zara вечернее", Description = "Чёрное мини-платье", Price = 7990m, Stock = 15, CategoryId = 1, ImageUrl = "https://placehold.co/300x400/c2185b/ffffff?text=Zara+Dress", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "Платье Mango летнее", Description = "Цветочный принт", Price = 4990m, Stock = 22, CategoryId = 1, ImageUrl = "https://placehold.co/300x400/f8bbd0/c2185b?text=Mango+Summer", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Туфли Aldo лодочки", Description = "Классические бежевые", Price = 9990m, Stock = 10, CategoryId = 2, ImageUrl = "https://placehold.co/300x400/e91e63/ffffff?text=Aldo+Heels", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Кроссовки Nike Air Max", Description = "Air Max 270", Price = 12990m, Stock = 18, CategoryId = 2, ImageUrl = "https://placehold.co/300x400/f8bbd0/c2185b?text=Nike+Air", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "Сумка Michael Kors", Description = "Кожаная среднего размера", Price = 24990m, Stock = 7, CategoryId = 3, ImageUrl = "https://placehold.co/300x400/c2185b/ffffff?text=MK+Bag", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 6, Name = "Рюкзак Furla", Description = "Розовый из эко-кожи", Price = 18990m, Stock = 9, CategoryId = 3, ImageUrl = "https://placehold.co/300x400/f8bbd0/c2185b?text=Furla", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<ProductDetails>().HasData(
            new ProductDetails { Id = 1, ProductId = 1, Manufacturer = "Zara", CountryOfOrigin = "Испания", Weight = 0.4, Dimensions = "Размер S/M/L", WarrantyMonths = 0 },
            new ProductDetails { Id = 2, ProductId = 4, Manufacturer = "Nike", CountryOfOrigin = "Вьетнам", Weight = 0.7, Dimensions = "EU 36-42", WarrantyMonths = 6 },
            new ProductDetails { Id = 3, ProductId = 5, Manufacturer = "Michael Kors", CountryOfOrigin = "Италия", Weight = 0.8, Dimensions = "30x25x12 см", WarrantyMonths = 0 }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, FullName = "Мария Стильная", Email = "maria@fashionmart.local", Phone = "+7-922-000-00-03", Address = "Москва, Тверская, 5", RegisteredAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
