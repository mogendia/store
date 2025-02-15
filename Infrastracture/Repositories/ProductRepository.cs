using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetProduct(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if(!string.IsNullOrWhiteSpace(brand))
            query = query.Where(b => b.Brand == brand);
        if(!string.IsNullOrWhiteSpace(type))
            query = query.Where(t => t.Type == type);
        query = sort switch
        {
            "priceDesc" => query.OrderByDescending(p => p.Price),
            "price" => query.OrderBy(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetBrand()
    {
        return await context.Products.Select(p => p.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetType()
    {
        return await context.Products.Select(p => p.Type)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await context.Products.FindAsync(id);
         
        
    }

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);

    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangeAsync()
    {
       return await context.SaveChangesAsync() > 0;
    }
}