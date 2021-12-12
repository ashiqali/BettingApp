using System;

namespace BettingApp.BLL.Dto
{
    public class CreateFixtureModel
    {
        public string type { get; set; }
        public int version { get; set; }
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public int id { get; set; }
        public string name { get; set; }
        public MarketModel[] markets { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public Sport sport { get; set; }
        public Location location { get; set; }
        public Timing timing { get; set; }
        public CompetitorModel[] competitors { get; set; }
    }

    public class Sport
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Location
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Timing
    {
        public DateTime scheduled_begin { get; set; }
        public string expected_duration { get; set; }
    }

    public class CompetitorModel
    {
        public int competitor_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public Child[] children { get; set; }
    }

    public class Child
    {
        public int player_id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }

    public class MarketModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
    }

}
