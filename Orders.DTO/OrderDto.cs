using Orders.Models;

namespace Orders.DTO
{
    public class OrderDto 
    {
        public int UserId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
