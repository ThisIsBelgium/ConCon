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
        List<string> artistNames = new List<string>();
        // GET: Map
        public ActionResult MapView(MapViewModel model)
        {
            artistNames.Add("korn");
            List<string> artists = ArtistSplit(artistNames);
            model.events = EventApiCall(model, artists);
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
        private List<Event> EventApiCall(MapViewModel model, List<string> artists)
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
                        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonString.Result);
                        foreach (Event select in rootObject.events)
                        {
                            events.Add(select);
                        }
                    }                    
                }
            }
            return events;
        }
    }
}
