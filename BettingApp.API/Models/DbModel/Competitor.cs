using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.API.Models
{
    public partial class Competitor
    {
        public int Id { get; set; }
        public int FixtureDetailsId { get; set; }
        public int PlayerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual FixtureDetail FixtureDetails { get; set; }
        public virtual LookupPlayer Player { get; set; }
    }
}
