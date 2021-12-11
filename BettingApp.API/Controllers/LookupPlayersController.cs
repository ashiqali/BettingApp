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
    public class LookupPlayersController : ControllerBase
    {
        private readonly BettingAppDBContext _context;

        public LookupPlayersController(BettingAppDBContext context)
        {
            _context = context;
        }

        // GET: api/LookupPlayers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LookupPlayer>>> GetLookupPlayers()
        {
            return await _context.LookupPlayers.ToListAsync();
        }

        // GET: api/LookupPlayers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LookupPlayer>> GetLookupPlayer(int id)
        {
            var lookupPlayer = await _context.LookupPlayers.FindAsync(id);

            if (lookupPlayer == null)
            {
                return NotFound();
            }

            return lookupPlayer;
        }

        // PUT: api/LookupPlayers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLookupPlayer(int id, LookupPlayer lookupPlayer)
        {
            if (id != lookupPlayer.Id)
            {
                return BadRequest();
            }

            _context.Entry(lookupPlayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookupPlayerExists(id))
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

        // POST: api/LookupPlayers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LookupPlayer>> PostLookupPlayer(LookupPlayer lookupPlayer)
        {
            _context.LookupPlayers.Add(lookupPlayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLookupPlayer", new { id = lookupPlayer.Id }, lookupPlayer);
        }

        // DELETE: api/LookupPlayers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLookupPlayer(int id)
        {
            var lookupPlayer = await _context.LookupPlayers.FindAsync(id);
            if (lookupPlayer == null)
            {
                return NotFound();
            }

            _context.LookupPlayers.Remove(lookupPlayer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LookupPlayerExists(int id)
        {
            return _context.LookupPlayers.Any(e => e.Id == id);
        }
    }
}
