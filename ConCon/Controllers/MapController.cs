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
            model.events = EventApiCall(artists);
            return View(model);
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
        private List<Event> EventApiCall(List<string> artists)
        {
            List<Event> events = new List<Event>();
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
                        foreach(Event select in result.events)
                        {
                            Event data = new Event();
                            data.performers = select.performers;
                            data.title = select.title;
                            data.venue.name = select.venue.name;
                            data.venue.location.lat = select.venue.location.lat;
                            data.venue.location.lon = select.venue.location.lon;
                            data.datetime_local = select.datetime_local;
                            data.venue.address = select.venue.address;
                            data.venue.city = select.venue.city;
                            data.venue.country = select.venue.country;
                            events.Add(data);
                        }
                       
                    }                    
                }
            }
            return events;
        }
    }
}
