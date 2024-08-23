using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.DTO;
using Orders.Models;

namespace Orders.Application
{
    public partial class Operations
    {

        public async Task  CreateOrderAsync(OrderDto orderDto)
        {
            var products = _context.Products.Where(p => orderDto.ProductIds.Contains(p.Id)).ToList();

            var order = new Order
            {
                IsDispatched = false,
                Products = products,
                UserId = orderDto.UserId
            };
            _context.Add(order);
            await using var transaction = await _context.Database.BeginTransactionAsync();
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await _paymentService.PayAsync(order);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            
        }
    }
}
