using SneakersAPI.Data;
using SneakersAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace SneakersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ItemsController (AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Item>>> CreateItems([FromBody] List<Item> items)
        {
            if (items == null || items.Count == 0)
            {
                return BadRequest("Нет элементов для добавления");
            }

            _context.Items.AddRange(items); // Добавляем все элементы сразу
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItems), items); // Возвращаем все добавленные элементы
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItems([FromBody] List<int> ids)
        {
            var items = await _context.Items.Where(item => ids.Contains(item.Id)).ToListAsync();

            if (items.Count == 0)
            {
                return NotFound();
            }

            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
