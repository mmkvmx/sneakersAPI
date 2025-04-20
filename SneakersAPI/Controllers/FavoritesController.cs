using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersAPI.Data;
using SneakersAPI.Models;

namespace SneakersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> Get(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<Favorite>> Add(Favorite item)
        {
            _context.Favorites.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Favorites.FindAsync(id);
            if (item == null) return NotFound();

            _context.Favorites.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
