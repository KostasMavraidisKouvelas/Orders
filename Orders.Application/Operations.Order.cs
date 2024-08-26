using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders.DTO;
using Orders.Models;

namespace Orders.Application
{
    public partial class Operations
    {

        public async Task  CreateOrderAsync(OrderDto orderDto,string recipientEmail)
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
                    var payment = new Payment
                    {
                        OrderId = order.Id,
                        IsProcessed = true,
                    };
                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    var invoice = await _invoiceGenerator.GenerateInvoice();
                    await _emailService.SendEmailAsync(recipientEmail, "Order Confirmation", "Your order has been placed", invoice);


                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch
            {
                throw; // Log exception
            }
        }

        public async Task<Order> SetOrderDispatchedAsync(int orderId)
        {
            try
            {
                var order = await _context.Orders.SingleOrDefaultAsync(c => c.Id == orderId);
                if (order == null)
                {
                    throw new Exception("Order not found");
                }

                order.IsDispatched = true;
                await _context.SaveChangesAsync();
                return order;
            }
            catch
            {
                throw; // Log exception
            }
        }
    }
}
