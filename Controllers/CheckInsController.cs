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
    public class CheckInsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckInsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckIns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckIns>>> GetCheckIns()
        {
            return await _context.CheckIns.ToListAsync();
        }

        // GET: api/CheckIns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckIns>> GetCheckIns(int id)
        {
            var checkIns = await _context.CheckIns.FindAsync(id);

            if (checkIns == null)
            {
                return NotFound();
            }

            return checkIns;
        }

        // PUT: api/CheckIns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckIns(int id, CheckIns checkIns)
        {
            if (id != checkIns.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkIns).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckInsExists(id))
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

        // POST: api/CheckIns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckIns>> PostCheckIns(CheckIns checkIns)
        {
            _context.CheckIns.Add(checkIns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckIns", new { id = checkIns.Id }, checkIns);
        }

        // DELETE: api/CheckIns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckIns(int id)
        {
            var checkIns = await _context.CheckIns.FindAsync(id);
            if (checkIns == null)
            {
                return NotFound();
            }

            _context.CheckIns.Remove(checkIns);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckInsExists(int id)
        {
            return _context.CheckIns.Any(e => e.Id == id);
        }
    }
}
