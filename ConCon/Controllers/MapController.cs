using ConCon.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ConCon.Controllers
{
    public class MapController : Controller
    {
        // GET: Map

        public ActionResult MapView(int id)
        {
            var artists = SearchSimilar(id);
            List<string> artistNames = new List<string>();
            foreach (var artist in artists)
            {
                artistNames.Add(artist.name);
            }
            List<string> splitArtists = ArtistSplit(artistNames);
            return View(EventApiCall(splitArtists));
        }
        private List<string> ArtistSplit(List<string> artistNames)
        {
            List<string> correctedNames = new List<string>();
            foreach (string artist in artistNames)
            {
                if (artist.Contains(" "))
                {
                    string correctedName = null;
                    
                    string[] splitName = artist.ToLower().Split();
                    foreach (string name in splitName)
                    {
                        correctedName += name + "-";
                    }
                    string artistName = correctedName.Remove(correctedName.Length - 1);
                    correctedNames.Add(artistName);
                }
                else
                {
                    string lowerArtist = artist.ToLower();
                    correctedNames.Add(lowerArtist);
                }
            }
            return correctedNames;
        }
        private List<EventViewModel> EventApiCall(List<string> artists)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };
            List<EventViewModel> events = new List<EventViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.seatgeek.com/2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string artist in artists)
                {
                    string url = "https://api.seatgeek.com/2/events?performers.slug=" + artist + "&client_id=ODExNjMyNnwxNDk5Nzg0NzQxLjEy";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        MapViewRootObject result = JsonConvert.DeserializeObject<MapViewRootObject>(jsonString.Result,settings);
                        foreach (EventViewModel Event in result.events)
                        {
                                events.Add(Event);
                        }
                    }
                }
            }
            return events;
        }
        private List<SimilarPerformerViewModel> SearchSimilar(int origId)
        {
            List<SimilarPerformerViewModel> ResultList = new List<SimilarPerformerViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.seatgeek.com/2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string url = "https://api.seatgeek.com/2/recommendations/performers?performers.id=" + origId + "&client_id=ODExNjMyNnwxNDk5Nzg0NzQxLjEy";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    SimilarPerformerRootObjectViewModel PerformerList = JsonConvert.DeserializeObject<SimilarPerformerRootObjectViewModel>(result.Result);
                    foreach (RecommendationViewModel rec in PerformerList.recommendations)
                    {
                        SimilarPerformerViewModel performer = new SimilarPerformerViewModel();
                        performer = rec.performer;
                        ResultList.Add(performer);
                    }
                }
            }
            return ResultList;
        }
    }
}