using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConCon.Models
{
    public class ArtistSearchViewModel
    {
        public string Search { get; set; } 
    }
    public class SearchSimilarViewModel
    {
        public string Search { get; set; }
    }
    public class GenreViewModel
    {
        public string name { get; set; }
        public bool primary { get; set; }
        public int id { get; set; }
        public string slug { get; set; }
    }
    public class PerformerViewModel
    {
        public object colors { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public object divisions { get; set; }
        public object home_venue_id { get; set; }
        public double popularity { get; set; }
        public string image { get; set; }
        public List<object> links { get; set; }
        public double score { get; set; }
        public bool has_upcoming_events { get; set; }
        public string name { get; set; }
        public List<GenreViewModel> genres { get; set; }
        public int id { get; set; }
        public object image_attribution { get; set; }
        public int num_upcoming_events { get; set; }
        public string short_name { get; set; }
        public object image_license { get; set; }
        public string url { get; set; }
    }
    public class ArtistSearchRootObjectViewModel
    {
        public List<PerformerViewModel> performers { get; set; }
    }
}