using BettingApp.DAL.DbContexts;
using BettingApp.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BettingAppDBContext _dbContext;

        public UnitOfWork(BettingAppDBContext dbContext, IFixtureRepository fixtureRepository,
            IFixtureDetailsRepository fixtureDetailsRepository, IMarketRepository marketRepository,
            ICompetitorRepository competitorRepository)
        {
            this._dbContext = dbContext;
            this.Fixture = fixtureRepository;
            this.FixtureDetails = fixtureDetailsRepository;
            this.Market = marketRepository;
            this.Competitor = competitorRepository;

        }

        public IFixtureRepository Fixture { get; set; }
        public IFixtureDetailsRepository FixtureDetails { get; set; }
        public IMarketRepository Market { get; set; }
        public ICompetitorRepository Competitor { get; set; }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
