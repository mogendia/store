using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProduct(string? brand , string? type, string? sort);
    Task<IReadOnlyList<string>> GetBrand();
    Task<IReadOnlyList<string>> GetType();

    Task<Product?> GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    
    // optional
    bool ProductExists(int id);
    Task<bool> SaveChangeAsync();



}