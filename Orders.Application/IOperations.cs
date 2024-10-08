﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.DTO;
using Orders.Models;

namespace Orders.Application
{
    public interface IOperations
    {
        public  Task ImportProductsAsync();
        public  Task<List<Product>> GetProductsAsync();

        public Task  CreateOrderAsync(OrderDto order,string recipientEmail);
        public Task<Product> GetProduct(int id);
        public Task<List<Order>> GetOrdersAsync();
        public Task<Order> SetOrderDispatchedAsync(int orderId);
        public Task ResendInvoiceAsync(int orderId);
    }
}
