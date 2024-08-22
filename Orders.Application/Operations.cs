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
                        // Save products to database
                    }
                }
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
