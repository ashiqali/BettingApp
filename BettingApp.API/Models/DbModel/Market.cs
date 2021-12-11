using System;
using System.Collections.Generic;

#nullable disable

namespace BettingApp.API.Models
{
    public partial class Market
    {
        public Market()
        {
            Fixtures = new HashSet<Fixture>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int FixtureId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
    }
}
