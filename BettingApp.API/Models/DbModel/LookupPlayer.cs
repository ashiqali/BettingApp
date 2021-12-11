using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.API.Models
{
    public partial class LookupPlayer
    {
        public LookupPlayer()
        {
            Competitors = new HashSet<Competitor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual LookupTeam Team { get; set; }
        public virtual ICollection<Competitor> Competitors { get; set; }
    }
}
