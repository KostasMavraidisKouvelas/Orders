using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Application
{
    public class PaymenMockService : IPaymentService
    {
        public async Task<bool> PayAsync(Order order)
        {
            // Mock the payment process
            
            return true;
        }
    }
}
