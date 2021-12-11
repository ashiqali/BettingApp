using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.API.Models
{
    public partial class LookupTeam
    {
        public LookupTeam()
        {
            LookupPlayers = new HashSet<LookupPlayer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<LookupPlayer> LookupPlayers { get; set; }
    }
}
