﻿using ConCon.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
<<<<<<< HEAD

=======
using System.Threading.Tasks;
>>>>>>> d316a838962124113e9d7b71c3a363becc825d96

namespace ConCon.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        
        public ActionResult MapView(int id)
        {
<<<<<<< HEAD

            artistNames.Add("korn");

            MapViewModel model = new MapViewModel();
            List<string> artists = ArtistSplit(artistNames);
            return View(EventApiCall(artists));
=======
            var artists = SearchSimilar(id);
            List<string> artistNames = new List<string>();
            foreach(var artist in artists)
            {
                artistNames.Add(artist.name);
            }    
            List<string> splitArtists = ArtistSplit(artistNames);
            return View(EventApiCall(splitArtists));
>>>>>>> d316a838962124113e9d7b71c3a363becc825d96
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
                        MapViewRootObject result = JsonConvert.DeserializeObject<MapViewRootObject>(jsonString.Result);
                        foreach (EventViewModel Event in result.events)
                        {
<<<<<<< HEAD
                            EventViewModel data = new EventViewModel();
                            data = selected;
                            events.Add(data);
=======
                            events.Add(Event);
>>>>>>> d316a838962124113e9d7b71c3a363becc825d96
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
