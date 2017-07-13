using ConCon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ConCon.Controllers
{
    public class ArtistSearchController : Controller
    {
        // GET: ArtistSearch
        public ActionResult Index()
        {
            return View();
        }
        //GET: Search
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult SearchResult(ArtistSearchViewModel model)
        {
            List<PerformerViewModel> ResultList = new List<PerformerViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.seatgeek.com/2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string url = "https://api.seatgeek.com/2/performers?q=" + model.Search + "&client_id=ODExNjMyNnwxNDk5Nzg0NzQxLjEy";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    ArtistSearchRootObjectViewModel PerformerList = JsonConvert.DeserializeObject<ArtistSearchRootObjectViewModel>(result.Result);
                    foreach (PerformerViewModel perf in PerformerList.performers)
                    {
                        PerformerViewModel performer = new PerformerViewModel();
                        performer.name = perf.name;
                        performer.id = perf.id;
                        performer.genres = perf.genres;
                        performer.score = perf.score;
                        performer.image = perf.image;
                        performer.popularity = perf.popularity;
                        performer.url = perf.url;

                        ResultList.Add(performer);

                    }
                }
            }
            return View(ResultList);
        }
        public async Task<ActionResult> SearchSimilar(SearchSimilarViewModel model)
        {
            model.Search = "1926";
            List<SimilarPerformerViewModel> ResultList = new List<SimilarPerformerViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.seatgeek.com/2/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string url = "https://api.seatgeek.com/2/recommendations/performers?performers.id=" + model.Search + "&client_id=ODExNjMyNnwxNDk5Nzg0NzQxLjEy";
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    SimilarPerformerRootObjectViewModel PerformerList = JsonConvert.DeserializeObject<SimilarPerformerRootObjectViewModel>(result);
                    foreach (RecommendationViewModel rec in PerformerList.recommendations)
                    {
                            SimilarPerformerViewModel performer = new SimilarPerformerViewModel();
                            performer.name = rec.performer.name;
                            performer.id = rec.performer.id;
                            performer.genres = rec.performer.genres;
                            performer.score = rec.performer.score; 
                            performer.image = rec.performer.image;
                            performer.url = rec.performer.url;

                            ResultList.Add(performer);

                        }
                    
                }
            }
            return View(ResultList);
        }
    }
}
    
