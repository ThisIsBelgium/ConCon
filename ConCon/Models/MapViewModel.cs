using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConCon.Models
{
    public class MapViewModel
    {
        public ApplicationUser user { get; set; }
        public List<Event> events { get; set; }
        public List<SimilarPerformerViewModel> performers { get; set; }
    }
    public class Meta
    {
        public int per_page { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int took { get; set; }
        public object geolocation { get; set; }
    }

    public class InHand
    {
    }

    public class Location
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Venue
    {
        public string url { get; set; }
        public double score { get; set; }
        public Location location { get; set; }
        public string address { get; set; }
        public int num_upcoming_events { get; set; }
        public string state { get; set; }
        public string name_v2 { get; set; }
        public string postal_code { get; set; }
        public int id { get; set; }
        public List<object> links { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string display_location { get; set; }
        public string slug { get; set; }
        public string extended_address { get; set; }
        public string timezone { get; set; }
        public string country { get; set; }
        public double popularity { get; set; }
        public bool has_upcoming_events { get; set; }
    }

    public class Stats
    {
        public int event_count { get; set; }
    }

    public class Genre
    {
        public string name { get; set; }
        public bool primary { get; set; }
        public string slug { get; set; }
        public int id { get; set; }
    }

    public class Taxonomy
    {
        public int? parent_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Images
    {
        public string huge { get; set; }
    }

    public class Performer
    {
        public Stats stats { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public object home_venue_id { get; set; }
        public string image { get; set; }
        public string short_name { get; set; }
        public object colors { get; set; }
        public List<Genre> genres { get; set; }
        public double popularity { get; set; }
        public int num_upcoming_events { get; set; }
        public string url { get; set; }
        public double score { get; set; }
        public string image_attribution { get; set; }
        public List<Taxonomy> taxonomies { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public Images images { get; set; }
        public object divisions { get; set; }
        public string image_license { get; set; }
        public bool has_upcoming_events { get; set; }
        public bool? primary { get; set; }
    }

    public class Taxonomy2
    {
        public int? parent_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Stats2
    {
        public int? listing_count { get; set; }
        public double? average_price { get; set; }
        public double? lowest_price_good_deals { get; set; }
        public double? lowest_price { get; set; }
        public double? highest_price { get; set; }
    }

    public class Event
    {
        public string datetime_utc { get; set; }
        public Venue venue { get; set; }
        public int id { get; set; }
        public List<Performer> performers { get; set; }
        public string datetime_local { get; set; }
        public bool time_tbd { get; set; }
        public string short_title { get; set; }
        public bool is_open { get; set; }
        public double popularity { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public double score { get; set; }
        public string type { get; set; }
        public List<Taxonomy2> taxonomies { get; set; }
        public bool datetime_tbd { get; set; }
        public bool date_tbd { get; set; }
        public string announce_date { get; set; }
        public List<object> links { get; set; }
        public string visible_until_utc { get; set; }
        public string created_at { get; set; }
        public Stats2 stats { get; set; }
    }

    public class MapViewRootObject
    {
        public Meta meta { get; set; }
        public InHand in_hand { get; set; }
        public List<Event> events { get; set; }
    }
}