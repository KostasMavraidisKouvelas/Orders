using System.Net.Http;
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


    }
}
