using eShopApi.Models;
using eShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    { 
            private readonly CartService _cartService;
                
            public CartController(CartService cartService)
            { 
                _cartService = cartService;
            }

            // POST api/cart
            [HttpPost]
            public async Task<ActionResult<string>> SaveCart([FromBody] Cart cart)
            {
            var result = await _cartService.SaveCart(cart);
                if (result == "Saved the Cart")
                  return CreatedAtAction(nameof(GetCartById), new { id = cart.CartId }, cart);
                else
                  return BadRequest(result);
            }

            // GET api/cart
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
            {
                var carts = await _cartService.GetAllCarts();
                return Ok(carts);
            }

            // GET api/cart/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Cart>> GetCartById(int id)
            {
                var cart = await _cartService.GetCartById(id);
                if (cart == null)
                    return NotFound();
                return Ok(cart);
            }

            // GET api/cart/user/5
            [HttpGet("user/{userId}")]
            public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId)
            {
               var carts = await _cartService.GetCartsByUserId(userId);
                 return Ok(carts);
            }

            // GET api/cart/user/5/id
            [HttpGet("user/{userId}/id")]
            public async Task<ActionResult<int?>> GetCartIdByUserId(int userId)
            {
             int cartId = await _cartService.GetCartIdByUserId(userId);
             if (cartId == null)
                return NotFound();
              return Ok(cartId);
            }

            // PUT api/cart/5
            [HttpPut("{id}")]
            public async Task<ActionResult<string>> UpdateCart(int id, [FromBody] Cart cart)
            {
                if (id != cart.CartId)
                    return BadRequest("Invalid ID");

                var result = await _cartService.UpdateCart(cart);
                if (result == "Updated the Cart")
                    return Ok(result);
                else
                    return BadRequest(result);
            }

            // DELETE api/cart/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<string>> DeleteCart(int id)
            {
                var result = await _cartService.DeleteCart(id);
                if (result == "Deleted the Cart")
                    return Ok(result);
                else
                    return BadRequest(result);
            }

            

            
        }
}
