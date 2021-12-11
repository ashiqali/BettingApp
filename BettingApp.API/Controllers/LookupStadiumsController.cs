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
    public class LookupStadiumsController : ControllerBase
    {
        private readonly BettingAppDBContext _context;

        public LookupStadiumsController(BettingAppDBContext context)
        {
            _context = context;
        }

        // GET: api/LookupStadiums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupStadium>>> GetLookupStadia()
        {
            return await _context.LookupStadia.ToListAsync();
        }

        // GET: api/LookupStadiums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupStadium>> GetLookupStadium(int id)
        {
            var lookupStadium = await _context.LookupStadia.FindAsync(id);

            if (lookupStadium == null)
            {
                return NotFound();
            }

            return lookupStadium;
        }

        // PUT: api/LookupStadiums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookupStadium(int id, LookupStadium lookupStadium)
        {
            if (id != lookupStadium.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookupStadium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupStadiumExists(id))
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

        // POST: api/LookupStadiums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LookupStadium>> PostLookupStadium(LookupStadium lookupStadium)
        {
            _context.LookupStadia.Add(lookupStadium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookupStadium", new { id = lookupStadium.Id }, lookupStadium);
        }

        // DELETE: api/LookupStadiums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupStadium(int id)
        {
            var lookupStadium = await _context.LookupStadia.FindAsync(id);
            if (lookupStadium == null)
            {
                return NotFound();
            }

            _context.LookupStadia.Remove(lookupStadium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupStadiumExists(int id)
        {
            return _context.LookupStadia.Any(e => e.Id == id);
        }
    }
}
