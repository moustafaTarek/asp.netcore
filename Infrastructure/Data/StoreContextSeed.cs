using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        private readonly static string _baseSeedDataPath = "../Infrastructure/Data/SeedData";

        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory iloggerFactory)
        {
            try
            {

            if(!storeContext.ProductBrands.Any())
            {
                var brand = File.ReadAllText(Path.Combine(_baseSeedDataPath,"brands.json"));

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brand);

                foreach(var item in brands){
                    storeContext.ProductBrands.Add(item);
                }

                await storeContext.SaveChangesAsync();
            }


            if(!storeContext.ProductTypes.Any())
            {
                var type = File.ReadAllText(Path.Combine(_baseSeedDataPath,"typed.json"));

                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(type);

                foreach(var item in productTypes){
                    storeContext.ProductTypes.Add(item);
                }

                await storeContext.SaveChangesAsync();
            }
            
            if(!storeContext.Products.Any())
            {
                var product = File.ReadAllText(Path.Combine(_baseSeedDataPath,"Products.json"));

                var products = JsonSerializer.Deserialize<List<Product>>(product);

                foreach(var item in products){
                    storeContext.Products.Add(item);
                }

                await storeContext.SaveChangesAsync();
            }
            
            }
            catch(Exception ex){

                var logger = iloggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}