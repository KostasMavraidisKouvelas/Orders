using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsDispatched { get; set; }
        public ICollection<Product> Products { get; set; }
        public Payment Payment { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public double TotalAmount
        {
            get
            {
                // Compute the total amount based on the products
                double total = 0;
                foreach (var product in Products)
                {
                    total += product.Price;
                }
                return total;
            }
        }
    }
}
