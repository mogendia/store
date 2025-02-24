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
            
            if (products?.Count > 0)
            {
                foreach (var product in products)
                {
                    await context.Products.AddAsync(product);
                }
                await context.SaveChangesAsync();
            }
        }
        if (!context.Deliveries.Any())
        {
            var DeliveryData = await File.ReadAllTextAsync("../Infrastracture/Data/SeedData/delivery.json");
            var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);
            if (delivery?.Count > 0) {
                foreach (var item in delivery)
                {
                    await context.Deliveries.AddAsync(item);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}