using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Application
{
    public interface IOperations
    {
        public  Task ImportProductsAsync();
        public  Task<List<Product>> GetProductsAsync();
    }
}
