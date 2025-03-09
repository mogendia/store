using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastracture.Services
{
    {
        public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
        {
            StripeConfiguration.ApiKey = config["StripeSettings:Secret"];
            var cart = await cartService.GetCartAsync(cartId);
            if(cart == null) return null;
            var shippingPrice = 0m;
            if (cart.DeliveryMethodId.HasValue)
            {
                if (deliveryMethod == null) return null;
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in cart.Items)
            {
                if (productItem == null)  return null;
                if (productItem.Price != item.Price)
                {
                    item.Price = productItem.Price;

                }
            }
                    +(long)(shippingPrice * 100));
             
            var service = new PaymentIntentService();
            PaymentIntent? intent = null;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                intent = await service.CreateAsync(options);
                cart.PaymentIntentId = intent.Id;
                cart.ClientSecret = intent.ClientSecret;
            } else
            {
                var options = new PaymentIntentUpdateOptions
                {
                };
                intent = await service.UpdateAsync(cart.PaymentIntentId, options);
            }
            await cartService.SetCartAsync(cart);
            return cart;
        }
    }
}
