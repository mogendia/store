using API.Dto;
using Core.Entities.OrderAggregate;

namespace API.Extensions
{
    public static class OrderMappingExtensions
    {
        public static OrderDto2 ToDto(this Order order)
        {
            return new OrderDto2
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                DeliveryMethod = order.DeliveryMethod.Description,
                Subtotal = order.Subtotal,
                PaymentIntentId = order.PaymentIntentId,
                ShippingPrice = order.DeliveryMethod.Price,
                OrderItems = order.OrderItems.Select(x=>x.ToDto()).ToList(),
                Status = order.Status.ToString(),
                PaymentSummary = order.PaymentSummary,
                Total = order.GetTotal()
            };
        }
        public static OrderItemDto ToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                ProductId = orderItem.ItemOrdered.ProductId,
                ProdcutName = orderItem.ItemOrdered.ProductName,
                ImgUrl = orderItem.ItemOrdered.ImgUrl,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
            };
        }
    }
}
