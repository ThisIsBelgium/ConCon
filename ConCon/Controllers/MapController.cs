using ConCon.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;


namespace ConCon.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult MapView(List<SimilarPerformerViewModel> performers)
        {
            List<string> artistName = new List<string>();
            foreach(SimilarPerformerViewModel performer in performers)
            {
                artistName.Add(performer.name);
            }
            List<string> artists = ArtistSplit(artistName);
            return View(EventApiCall(artists));
        }
        private List<string> ArtistSplit(List<string> artistNames)
        {
            List<string> correctedNames = new List<string>();
            foreach (string artist in artistNames)
            {
                if (artist.Contains(" "))
                {
                    string correctedName = null;
                    string[] splitName = artist.Split();
                    foreach (string name in splitName)
                    {
                        correctedName += name + "-";
                    }
                    string artistName = correctedName.Remove(correctedName.Length - 1);
                    correctedNames.Add(artistName);
                }
                else
                {
                    correctedNames.Add(artist);
                }
            }
            return correctedNames;
        }
        private List<EventViewModel> EventApiCall(List<string> artists)
        {
            List<EventViewModel> events = new List<EventViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.seatgeek.com/2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string artist in artists)
                {
                    string url = "events?performers.slug=" + artist + "&client_id=ODExNjMyNnwxNDk5Nzg0NzQxLjEy";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<MapViewRootObject>(jsonString.Result);
                        events = result.events;
                    }
                                        
                }
            }
            return events;
        }
    }
}
