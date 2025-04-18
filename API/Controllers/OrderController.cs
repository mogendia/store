﻿using API.Dto;
using API.Extensions;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrderController (ICartService cartService, IUnitOfWork unit) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder (OrderDto orderDto)
        {
            var email = User.GetEmail();
            var cart = await cartService.GetCartAsync(orderDto.CartId);
            if (cart == null) return BadRequest("cart not found");
            if (cart.PaymentIntentId == null) return BadRequest("no payment intent found");
            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItems = await unit.Repository<Product>().GetByIdAsync(item.ProductId);
                if (productItems == null) return BadRequest("Product item not found");
                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = item.ProductId,
                    ProductName = productItems.Name,
                    ImgUrl = productItems.ImgUrl
                };
                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = productItems.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }
            var deliveryMethod = await unit.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod == null) return BadRequest("Delivery method not selected");
            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderDto.ShippingAddress,
                Subtotal = items.Sum(x => x.Price * x.Quantity),
                PaymentSummary = orderDto.PaymentSummary,
                PaymentIntentId = cart.PaymentIntentId,
                BuyerEmail = email
            };
            unit.Repository<Order>().Add(order);
            if (await unit.Complete()) return order;
            return BadRequest("Problem in creating order");

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto2>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());
            var orders = await unit.Repository<Order>().GetAllAsyncWithSpec(spec);
            var ordersToReturn = orders.Select(x => x.ToDto()).ToList();
            return Ok(ordersToReturn);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderDto2>>> GetOrderById(int id)
        {
            var spec = new OrderSpecification(User.GetEmail(),id);
            var order = await unit.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null) return NotFound();
            return Ok(order.ToDto());
        }

    }
}
