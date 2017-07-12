using ConCon.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConCon.Controllers
{
    public class MapController : Controller
    {
        List<string> artistNames = new List<string>();
        // GET: Map
        public ActionResult MapView(MapViewModel model)
        {
            artistNames.Add("tool");
            List<string> artists = ArtistSplit(artistNames);
            EventApiCall(model, artists);
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
                        correctedName += name + "+";
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
        private void EventApiCall(MapViewModel model, List<string> artists)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.songkick.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                foreach (string artist in artists)
                {
                    string url = "3.0/events.json?apikey={}&artist_name=" + artist;
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        model.events.Add(response);
                    }
                }
            }
        }
    }
}
