using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.DAL.DbEntities
{
    public partial class LookupStadium
    {
        public LookupStadium()
        {
            FixtureDetails = new HashSet<FixtureDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<FixtureDetail> FixtureDetails { get; set; }
    }
}
