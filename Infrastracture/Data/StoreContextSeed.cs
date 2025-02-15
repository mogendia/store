using System.Text.Json;
using Core.Entities;

namespace Infrastracture.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productData = await File.ReadAllTextAsync("../Infrastracture/Data/SeedData/products.json");
            // Serialize (convert object to json)
            // Deserialize (convert json to object)

            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            //if (products == null) return;
            //context.Products.AddRange(products);
            //await context.SaveChangesAsync();
            if (products?.Count > 0) {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }


        }
    }
}