using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Orders.Models;

namespace Orders.Application
{
    public interface IPaymentServivce
    {
         Task<bool> PayAsync(Order order);
    }
}
