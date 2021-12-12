using BettingApp.DAL.DbContexts;
using BettingApp.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.DAL.Repository
{
    public class MarketRepository : GenericRepository<Market>, IMarketRepository
    {
        public MarketRepository(BettingAppDBContext context) : base(context)
        {

        }
    }
}
