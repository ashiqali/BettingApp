using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.DAL.DbEntities
{
    public partial class FixtureDetail
    {
        public FixtureDetail()
        {
            Competitors = new HashSet<Competitor>();
        }

        public int Id { get; set; }
        public int FixtureId { get; set; }
        public int SportId { get; set; }
        public int LocationId { get; set; }
        public DateTime ScheduledBegin { get; set; }
        public TimeSpan ExpectedDuration { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Fixture Fixture { get; set; }
        public virtual LookupStadium Location { get; set; }
        public virtual LookupSport Sport { get; set; }
        public virtual ICollection<Competitor> Competitors { get; set; }
    }
}
