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
<<<<<<< HEAD
=======
        List<string> artistNames = new List<string>();
       
>>>>>>> 05361ce1c63d3964d9e3fd0d08475f547d96b898
        // GET: Map
        public ActionResult MapView(List<SimilarPerformerViewModel> performers)
        {
<<<<<<< HEAD
            List<string> artistName = new List<string>();
            foreach(SimilarPerformerViewModel performer in performers)
            {
                artistName.Add(performer.name);
            }
            List<string> artists = ArtistSplit(artistName);
=======
<<<<<<< HEAD
            artistNames.Add("Nine Inch Nails");
=======
            artistNames.Add("korn");
>>>>>>> 85a18744235ddcab87f0cd4f92a5f43e762aa157
            MapViewModel model = new MapViewModel();
            List<string> artists = ArtistSplit(artistNames);
>>>>>>> 05361ce1c63d3964d9e3fd0d08475f547d96b898
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
