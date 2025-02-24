using Core.Entities.OrderAggregate;
using Core.Entities;

namespace API.Dto
{
    public class OrderDto
    {
        public string CartId{ get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public PaymentSummary PaymentSummary { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
      
    }
}
