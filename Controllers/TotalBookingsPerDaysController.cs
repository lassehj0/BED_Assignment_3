using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;

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
            TypeAdapterConfig<TotalBookingsPerDay, TotalBookingsPerDay_Kitchen_DTO>.NewConfig().IgnoreNullValues(true);
            TypeAdapterConfig<TotalBookingsPerDay_Reception_DTO, TotalBookingsPerDay>.NewConfig().IgnoreNullValues(true);


        }

        // GET: api/TotalBookingsPerDays
        [HttpGet]
        public async Task<ActionResult<TotalBookingsPerDay_Kitchen_DTO>> GetTotalBookingsPerDay()
        {
            List<TotalBookingsPerDay> listOfBookingsPerDays = await _context.TotalBookingsPerDay.OrderByDescending(x => x.Date).ToListAsync();

            var latestBooking = listOfBookingsPerDays.FirstOrDefault();
            if (latestBooking == null)
            {
                return NotFound();
            }
            
            var result = latestBooking.Adapt<TotalBookingsPerDay_Kitchen_DTO>();
            result.TotalGuests = result.TotalAdults + result.TotalChildren;

            List<CheckIns> checkInsList = await _context.CheckIns.Where(x => x.Date == latestBooking.Date).ToListAsync();

            foreach (var checkIn in checkInsList)
            {
                result.CheckedInAdults += checkIn.Adults;
                result.CheckedInChildren += checkIn.Children;
            }

            result.RemainingAdults = result.TotalAdults - result.CheckedInAdults;
            result.RemainingChildren = result.TotalChildren - result.CheckedInChildren;
            result.RemainingGuests = result.TotalGuests - (result.CheckedInAdults + result.CheckedInChildren);
            
            return result;
        }

        // GET: api/TotalBookingsPerDays/5
        [HttpGet("{date}")]
        public async Task<ActionResult<TotalBookingsPerDay_Kitchen_DTO>> GetTotalBookingsPerDay(DateTime date)
        {

            var latestBooking = _context.CheckIns.Where(x => x.Date == date).FirstOrDefault();
            if (!(latestBooking.Date == date))
            {
                return NotFound();
            }
            var result = latestBooking.Adapt<TotalBookingsPerDay_Kitchen_DTO>();

            List<CheckIns> checkInsList = await _context.CheckIns.Where(x => x.Date == latestBooking.Date).ToListAsync();

            foreach (var checkIn in checkInsList)
            {
                result.CheckedInAdults += checkIn.Adults;
                result.CheckedInChildren += checkIn.Children;
            }

            result.RemainingAdults = result.TotalAdults - result.CheckedInAdults;
            result.RemainingChildren = result.TotalChildren - result.CheckedInChildren;
            result.RemainingGuests = result.TotalGuests - (result.CheckedInAdults + result.CheckedInChildren);
            return result;
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
        [Authorize("ReceptionOnly")]
        [HttpPost]
        public async Task<ActionResult<TotalBookingsPerDay>> PostTotalBookingsPerDay(TotalBookingsPerDay_Reception_DTO totalBookingsPerDay_DTO)
        {

            var totalBookingsPerDay = totalBookingsPerDay_DTO.Adapt<TotalBookingsPerDay>();
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
