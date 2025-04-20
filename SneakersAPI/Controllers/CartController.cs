using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersAPI.Data;
using SneakersAPI.Models;

namespace SneakersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> Get(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return Ok(new List<CartItem>());  // Возвращаем пустой список, если корзина пуста
            }

            return Ok(cartItems);  // Возвращаем найденные товары в корзине
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<CartItem>> Add(CartItem item)
        {
            _context.CartItems.Add(item);   
            await _context.SaveChangesAsync();
            return Ok(item);    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.CartItems.Remove(item);    
            await _context.SaveChangesAsync();  
            return NoContent(); 
        }
    }
}
