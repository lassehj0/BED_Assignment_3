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
            TypeAdapterConfig<CheckIns, CheckInsDTO>.NewConfig();
            TypeAdapterConfig<CheckInsDTO, CheckIns>.NewConfig();
        }

        // GET: api/CheckIns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckIns>>> GetCheckIns()
        {
            return await _context.CheckIns.ToListAsync();
        }

        [HttpGet("{Date}")]
        public async Task<ActionResult<CheckInsDTO>> GetCheckInsDTO(int date)
        {
            var checkIns = await _context.CheckIns.FindAsync(date);

            if (checkIns == null)
            {
                return NotFound();
            }

            return checkIns.Adapt<CheckInsDTO>();
        }

        // POST: api/CheckIns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CheckIns>> PostCheckIns(CheckInsDTO checkInsDTO)
        {
            var checkIns = checkInsDTO.Adapt<CheckIns>();
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
