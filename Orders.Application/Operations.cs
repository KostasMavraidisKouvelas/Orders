using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using Orders.DataAccess;
using Orders.Models;

namespace Orders.Application
{
    public partial class Operations : IOperations
    {
        private readonly OrdersDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public Operations(OrdersDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task ImportProductsAsync()
        {
            try
            {
                var domains = _context.ProductSources.Where(c => c.IsActive).ToList();
                var products = new List<Product>();

                foreach (var domain in domains)
                {
                    var response = await _httpClient.GetAsync($"{domain.Domain}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var deserializedProducts = JsonConvert.DeserializeObject<List<Product>>(responseContent);
                        products.AddRange(deserializedProducts);
                    }
                }

                var existingProducts = await _context.Products.Where(c=>products.Select(c=>c.Id).Contains(c.Id)).ToDictionaryAsync(c=>c.Id,c=>c);

                foreach (var product in products)
                {
                    existingProducts.TryGetValue(product.Id, out var existingProduct);
                    if (existingProduct != null)
                    {
                        // Update existing product
                        _context.Entry(existingProduct).CurrentValues.SetValues(product);
                    }
                    else
                    {
                        // Insert new product
                        _context.Products.Add(product);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw; // Log exception
            }
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch
            {
                throw; // Log exception
            }
        }
    }
}
