using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Application
{
    public class PaymentService : IPaymentServivce
    {
        public async Task<bool> PayAsync(Order order)
        {
            // Mock the payment process
            await Task.Delay(0);
            return true;
        }
    }
}
