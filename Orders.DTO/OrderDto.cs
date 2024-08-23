using Orders.Models;

namespace Orders.DTO
{
    public class OrderDto 
    {
        public Guid UserId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
