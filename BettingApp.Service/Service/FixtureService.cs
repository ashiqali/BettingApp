using BettingApp.BLL.Dto;
using BettingApp.DAL.DbEntities;
using BettingApp.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.BLL
{
    public class FixtureService : IFixtureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FixtureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> CreateFixture(CreateFixtureModel model)
        {
            try
            {
                //Add Fixture
                Fixture fixture = new Fixture();
                fixture.Name = model.payload.name;
                fixture.CreatedDate = DateTime.UtcNow;
                await _unitOfWork.Fixture.Add(fixture);
                _unitOfWork.Complete();
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
                await _unitOfWork.Market.AddRange(marketList);

                //Add FixtureDetails
                FixtureDetail fixtureDetail = new FixtureDetail();
                fixtureDetail.FixtureId = fixture.Id;
                fixtureDetail.SportId = model.payload.metadata.sport.id;
                fixtureDetail.LocationId = model.payload.metadata.location.id;
                fixtureDetail.ScheduledBegin = model.payload.metadata.timing.scheduled_begin;
                fixtureDetail.ExpectedDuration = TimeSpan.Parse(model.payload.metadata.timing.expected_duration);
                fixtureDetail.CreatedDate = DateTime.UtcNow;
                await _unitOfWork.FixtureDetails.Add(fixtureDetail);
                _unitOfWork.Complete();

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
                await _unitOfWork.Competitor.AddRange(competitorList);
                _unitOfWork.Complete();

                return "Success";//   CreatedAtAction("GetFixture", new { id = fixture.Id }, fixture);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
