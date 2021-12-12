using BettingApp.DAL.Repository;

namespace BettingApp.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFixtureRepository Fixture { get; set; }
        IFixtureDetailsRepository FixtureDetails { get; set; }
        IMarketRepository Market { get; set; }
        ICompetitorRepository Competitor { get; set; }
        int Complete();

    }
}