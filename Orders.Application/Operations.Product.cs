using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public partial class Operations
    {
        public async Task<Product> GetProduct(int Id)
        {
            try
            {
                return await _context.Products.SingleOrDefaultAsync(c=>c.Id==Id);
            }
            catch
            {
                throw; // Log exception
            }
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

                var existingProducts = await _context.Products.Where(c => products.Select(c => c.Id).Contains(c.Id)).ToDictionaryAsync(c => c.Id, c => c);

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
