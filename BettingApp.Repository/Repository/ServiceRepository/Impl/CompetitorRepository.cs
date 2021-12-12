using BettingApp.DAL.DbContexts;
using BettingApp.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.DAL.Repository
{
    public class CompetitorRepository : GenericRepository<Competitor>, ICompetitorRepository
    {
        public CompetitorRepository(BettingAppDBContext context) : base(context)
        {

        }
    }
}
