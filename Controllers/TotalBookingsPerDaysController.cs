using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalBookingsPerDaysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TotalBookingsPerDaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TotalBookingsPerDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TotalBookingsPerDay>>> GetTotalBookingsPerDay()
        {
            return await _context.TotalBookingsPerDay.ToListAsync();
        }

        // GET: api/TotalBookingsPerDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TotalBookingsPerDay>> GetTotalBookingsPerDay(int id)
        {
            var totalBookingsPerDay = await _context.TotalBookingsPerDay.FindAsync(id);

            if (totalBookingsPerDay == null)
            {
                return NotFound();
            }

            return totalBookingsPerDay;
        }

        // PUT: api/TotalBookingsPerDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTotalBookingsPerDay(int id, TotalBookingsPerDay totalBookingsPerDay)
        {
            if (id != totalBookingsPerDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(totalBookingsPerDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalBookingsPerDayExists(id))
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

        // POST: api/TotalBookingsPerDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TotalBookingsPerDay>> PostTotalBookingsPerDay(TotalBookingsPerDay totalBookingsPerDay)
        {
            _context.TotalBookingsPerDay.Add(totalBookingsPerDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTotalBookingsPerDay", new { id = totalBookingsPerDay.Id }, totalBookingsPerDay);
        }

        // DELETE: api/TotalBookingsPerDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTotalBookingsPerDay(int id)
        {
            var totalBookingsPerDay = await _context.TotalBookingsPerDay.FindAsync(id);
            if (totalBookingsPerDay == null)
            {
                return NotFound();
            }

            _context.TotalBookingsPerDay.Remove(totalBookingsPerDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TotalBookingsPerDayExists(int id)
        {
            return _context.TotalBookingsPerDay.Any(e => e.Id == id);
        }
    }
}
