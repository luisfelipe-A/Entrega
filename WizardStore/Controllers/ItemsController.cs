using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizardStoreAPI.Models;
using WizardStoreAPI.Data;

namespace WizardStoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/MagicItems")]
    public class MagicItemsController : ControllerBase
    {
        private readonly WizardStoreContext _context;

        public MagicItemsController(WizardStoreContext context)
        {
            _context = context;
        }

        // GET: api/MagicItems/all
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetAllItems()
        {
          if (_context.MagicItems == null)
          {
              return NotFound();
          }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/lowstock
        [HttpGet("LowStock")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetLowStockItems()
        {
          if (_context.MagicItems == null)
          {
              return NotFound();
          }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .Where(mi=>mi.Quantity < MagicItem.GetLowStockAmount())
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/favoredlowstock
        //returns any favored as high priority items that are low on stock
        [HttpGet("FavoredLowStock")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetLowStockFavoredItems()
        {
          if (_context.MagicItems == null)
          {
              return NotFound();
          }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .Where(mi=>mi.Quantity < MagicItem.GetLowStockAmount() && mi.IsFavored)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MagicItem>> GetItem(string name)
        {
          if (_context.MagicItems == null)
          {
              return NotFound();
          }
            var item = await _context.MagicItems.FindAsync(name);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/MagicItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string name, MagicItem item)
        {
            if (name != item.Name)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MagicItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MagicItem>> PostItem(MagicItem item)
        {
          if (_context.MagicItems == null)
          {
              return Problem("Entity set 'WizardStoreContext.MagicItems'  is null.");
          }
            _context.MagicItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { name = item.Name }, item);
        }

        // DELETE: api/MagicItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string name)
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            var item = await _context.MagicItems.FindAsync(name);
            if (item == null)
            {
                return NotFound();
            }

            _context.MagicItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string name)
        {
            return (_context.MagicItems?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}