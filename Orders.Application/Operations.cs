using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Orders.DataAccess;

namespace Orders.Application
{
    public partial class Operations : IOperations
    {
        private readonly OrdersDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public Operations(OrdersDbContext context, HttpClient httpClient,IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task ImportProducts()
        {
            _httpClient.GetAsync("https://api.com/products");
        }
    }
}
