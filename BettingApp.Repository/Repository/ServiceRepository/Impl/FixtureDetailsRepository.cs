using BettingApp.DAL.DbContexts;
using BettingApp.DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.DAL.Repository
{
    public class FixtureDetailsRepository : GenericRepository<FixtureDetail>, IFixtureDetailsRepository
    {
        public FixtureDetailsRepository(BettingAppDBContext context) : base(context)
        {

        }
    }
}
