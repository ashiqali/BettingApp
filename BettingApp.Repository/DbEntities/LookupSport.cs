using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.DAL.DbEntities
{
    public partial class LookupSport
    {
        public LookupSport()
        {
            FixtureDetails = new HashSet<FixtureDetail>();
        }

        public int Id { get; set; }
        public string Sports { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<FixtureDetail> FixtureDetails { get; set; }
    }
}
