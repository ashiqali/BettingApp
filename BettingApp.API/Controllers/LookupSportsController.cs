using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BettingApp.API.DbContext;
using BettingApp.API.Models;

namespace BettingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupSportsController : ControllerBase
    {
        private readonly BettingAppDBContext _context;

        public LookupSportsController(BettingAppDBContext context)
        {
            _context = context;
        }

        // GET: api/LookupSports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupSport>>> GetLookupSports()
        {
            return await _context.LookupSports.ToListAsync();
        }

        // GET: api/LookupSports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupSport>> GetLookupSport(int id)
        {
            var lookupSport = await _context.LookupSports.FindAsync(id);

            if (lookupSport == null)
            {
                return NotFound();
            }

            return lookupSport;
        }

        // PUT: api/LookupSports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookupSport(int id, LookupSport lookupSport)
        {
            if (id != lookupSport.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookupSport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupSportExists(id))
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

        // POST: api/LookupSports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LookupSport>> PostLookupSport(LookupSport lookupSport)
        {
            _context.LookupSports.Add(lookupSport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookupSport", new { id = lookupSport.Id }, lookupSport);
        }

        // DELETE: api/LookupSports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupSport(int id)
        {
            var lookupSport = await _context.LookupSports.FindAsync(id);
            if (lookupSport == null)
            {
                return NotFound();
            }

            _context.LookupSports.Remove(lookupSport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupSportExists(int id)
        {
            return _context.LookupSports.Any(e => e.Id == id);
        }
    }
}
