using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;
using MapsterMapper;
using Mapster;
using Assignment3.DTO;
using Microsoft.AspNetCore.Authorization;

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
            TypeAdapterConfig<List<CheckIn>, List<CheckInsDTO>>.NewConfig();
            TypeAdapterConfig<CheckInsDTO, CheckIn>.NewConfig();
        }

        // GET: api/CheckIns
        [Authorize("ReceptionOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckIn>>> GetCheckIns()
        {
            return await _context.CheckIns.ToListAsync();
        }

        [HttpGet("{Date}")]
        public async Task<ActionResult<List<CheckInsDTO>>> GetCheckInsDTO(DateTime date)
        {
            var checkIns = _context.CheckIns.Where(c => c.Date == date).ToListAsync();

            if (checkIns == null)
            {
                return NotFound();
            }

            return checkIns.Adapt<List<CheckInsDTO>>();
        }

        // POST: api/CheckIns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize("WaiterOnly")]
        [HttpPost]
        public async Task<ActionResult<CheckIn>> PostCheckIn(CheckInsDTO checkInsDTO)
        {
            var checkIns = checkInsDTO.Adapt<CheckIn>();
            checkIns.Date = new DateTime();
            _context.CheckIns.Add(checkIns);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckIns", new { id = checkIns.Id }, checkIns);
        }

        private bool CheckInsExists(int id)
        {
            return _context.CheckIns.Any(e => e.Id == id);
        }
    }
}
