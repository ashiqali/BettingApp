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
    public class LookupTeamsController : ControllerBase
    {
        private readonly BettingAppDBContext _context;

        public LookupTeamsController(BettingAppDBContext context)
        {
            _context = context;
        }

        // GET: api/LookupTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupTeam>>> GetLookupTeams()
        {
            return await _context.LookupTeams.ToListAsync();
        }

        // GET: api/LookupTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupTeam>> GetLookupTeam(int id)
        {
            var lookupTeam = await _context.LookupTeams.FindAsync(id);

            if (lookupTeam == null)
            {
                return NotFound();
            }

            return lookupTeam;
        }

        // PUT: api/LookupTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookupTeam(int id, LookupTeam lookupTeam)
        {
            if (id != lookupTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookupTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupTeamExists(id))
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

        // POST: api/LookupTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LookupTeam>> PostLookupTeam(LookupTeam lookupTeam)
        {
            _context.LookupTeams.Add(lookupTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookupTeam", new { id = lookupTeam.Id }, lookupTeam);
        }

        // DELETE: api/LookupTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupTeam(int id)
        {
            var lookupTeam = await _context.LookupTeams.FindAsync(id);
            if (lookupTeam == null)
            {
                return NotFound();
            }

            _context.LookupTeams.Remove(lookupTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupTeamExists(int id)
        {
            return _context.LookupTeams.Any(e => e.Id == id);
        }
    }
}
