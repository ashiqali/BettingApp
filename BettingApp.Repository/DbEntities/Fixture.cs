using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.DAL.DbEntities
{
    public partial class Fixture
    {
        public Fixture()
        {
            FixtureDetails = new HashSet<FixtureDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? WinnerId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Market Winner { get; set; }
        public virtual ICollection<FixtureDetail> FixtureDetails { get; set; }
    }
}
