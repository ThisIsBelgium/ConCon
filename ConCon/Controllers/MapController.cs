using ConCon.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ConCon.Controllers
{
    public class MapController : Controller
    {
        List<string> artistNames = new List<string>();
       
        // GET: Map
        public ActionResult MapView()
        {
            MapViewModel model = new MapViewModel();
            List<string> artists = ArtistSplit(artistNames);
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
                        foreach(var selected in result.events)
                        {
                            EventViewModel data = new EventViewModel();
                            data.performers = selected.performers;
                            data.title = selected.title;
                            data.venue = selected.venue;
                            events.Add(data);
                        }
                    }                    
                }
            }
            return events;
        }
    }
}
