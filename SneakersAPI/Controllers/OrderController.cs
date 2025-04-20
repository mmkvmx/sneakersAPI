using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersAPI.Data;
using SneakersAPI.Models;
using System.Text.Json;

namespace SneakersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.Items == null || orderRequest.Items.Count == 0)
                return BadRequest("Данные заказа пусты.");

            var itemsJson = JsonSerializer.Serialize(orderRequest.Items);
            var total = orderRequest.Items.Sum(i => i.Price);

            var order = new Order
            {
                UserId = orderRequest.UserId,
                ItemsJson = itemsJson,
                TotalPrice = total
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Очистка корзины (по желанию)
            var cartItems = _context.CartItems.Where(i => i.UserId == orderRequest.UserId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        // Вложенный вспомогательный класс для POST запроса
        public class OrderRequest
        {
            public int UserId { get; set; }
            public List<CartItem> Items { get; set; }
        }
    }
}
