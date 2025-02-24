﻿using Core.Entities.OrderAggregate;
using Core.Entities;

namespace API.Dto
{
    public class OrderDto2
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } 
        public required string BuyerEmail { get; set; }
        public required ShippingAddress ShippingAddress { get; set; } 
        public required PaymentSummary PaymentSummary { get; set; } 
        public required List<OrderItemDto> OrderItems { get; set; } 
        public required string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Subtotal { get; set; }
        public required string Status { get; set; } 
        public required string PaymentIntentId { get; set; }
        public decimal Total { get; set; }
    }
 
}
