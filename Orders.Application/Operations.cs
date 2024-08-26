using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;
        private readonly IInvoiceGenerator _invoiceGenerator;
        public Operations(OrdersDbContext context, HttpClient httpClient, IConfiguration configuration, IPaymentService paymentService,IEmailService emailService,UserManager<User> userManager,IInvoiceGenerator invoiceGenerator)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
            _paymentService = paymentService;
            _emailService = emailService;
            _invoiceGenerator = invoiceGenerator;
        }
    }
}
