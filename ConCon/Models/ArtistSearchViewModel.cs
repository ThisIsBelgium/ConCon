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
        public double popularity { get; set; }
        public string image { get; set; }
        public List<object> links { get; set; }
        public double score { get; set; }
        public string name { get; set; }
        public List<GenreViewModel> genres { get; set; }
        public int id { get; set; }
        public string url { get; set; }
    }
    public class ArtistSearchRootObjectViewModel
    {
        public List<PerformerViewModel> performers { get; set; }
    }
}