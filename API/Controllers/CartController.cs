using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController(ICartService _cart) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id) 
        {
            var cart = await _cart.GetCartAsync(id);
            return Ok(cart ?? new ShoppingCart { Id = id});
        }
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart) 
        {
            var updatedCart = await _cart.SetCartAsync(cart);
            if(updatedCart == null) return BadRequest("Problem updating cart");
            return Ok(updatedCart);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string id) 
        {
         var cart = await _cart.DeleteCartAsync(id);
         if(!cart) return BadRequest("Problem deleting cart");
         return Ok();
        }
    }
}
