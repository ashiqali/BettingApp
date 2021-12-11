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
    public class FixturesController : ControllerBase
    {
        private readonly BettingAppDBContext _context;

        public FixturesController(BettingAppDBContext context)
        {
            _context = context;
        }

        [HttpPost(nameof(CreateFixtureModel))]
        public async Task<IActionResult> CreateFixture(CreateFixtureModel model)
        {

            //Add Fixture
            Fixture fixture = new Fixture();
            fixture.Name = model.payload.name;
            fixture.CreatedDate = DateTime.UtcNow;
            _context.Fixtures.Add(fixture);
            await _context.SaveChangesAsync();
            //

            //Add Market

            List<Market> marketList = new List<Market>();
            foreach (var item in model.payload.markets)
            {
                Market market = new Market();
                market.FixtureId = fixture.Id;
                market.Title = item.title;
                market.Price = (decimal)item.price;
                market.CreatedDate = DateTime.UtcNow;
                marketList.Add(market);
            }
            _context.Markets.AddRange(marketList);

            //Add FixtureDetails
            FixtureDetail fixtureDetail = new FixtureDetail();
            fixtureDetail.FixtureId = fixture.Id;
            fixtureDetail.SportId = model.payload.metadata.sport.id;
            fixtureDetail.LocationId = model.payload.metadata.location.id;
            fixtureDetail.ScheduledBegin = model.payload.metadata.timing.scheduled_begin;
            fixtureDetail.ExpectedDuration = TimeSpan.Parse(model.payload.metadata.timing.expected_duration);
            fixtureDetail.CreatedDate = DateTime.UtcNow;
            _context.FixtureDetails.Add(fixtureDetail);
            await _context.SaveChangesAsync();

            //Add Competitors

            List<Competitor> competitorList = new List<Competitor>();
            foreach (var comp in model.payload.metadata.competitors)
            {
                foreach (var player in comp.children)
                {
                    Competitor competitor = new Competitor();
                    competitor.FixtureDetailsId = fixtureDetail.Id;
                    competitor.PlayerId = player.player_id;
                    competitor.CreatedDate = DateTime.UtcNow;
                    competitorList.Add(competitor);
                }
            }
            _context.Competitors.AddRange(competitorList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFixture", new { id = fixture.Id }, fixture);
        }


        // GET: api/Fixtures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fixture>>> GetFixtures()
        {
            return await _context.Fixtures.ToListAsync();
        }

        // GET: api/Fixtures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fixture>> GetFixture(int id)
        {
            var fixture = await _context.Fixtures.Include(x => x.FixtureDetails).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (fixture == null)
            {
                return NotFound();
            }

            return fixture;
        }

        // PUT: api/Fixtures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFixture(int id, Fixture fixture)
        {
            if (id != fixture.Id)
            {
                return BadRequest();
            }

            _context.Entry(fixture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixtureExists(id))
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

        // POST: api/Fixtures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fixture>> PostFixture(Fixture fixture)
        {
            _context.Fixtures.Add(fixture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFixture", new { id = fixture.Id }, fixture);
        }

        // DELETE: api/Fixtures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixture(int id)
        {
            var fixture = await _context.Fixtures.FindAsync(id);
            if (fixture == null)
            {
                return NotFound();
            }

            _context.Fixtures.Remove(fixture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FixtureExists(int id)
        {
            return _context.Fixtures.Any(e => e.Id == id);
        }
    }
}
