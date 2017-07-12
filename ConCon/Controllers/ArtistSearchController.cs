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
      public ActionResult Search()
        {
            return View();
        }

        public ActionResult SearchResult(ArtistSearchViewModel model, PerformerViewModel performer)
        {
            model.Search = "Tool";
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
                    RootObject PerformerList = JsonConvert.DeserializeObject<RootObject>(result.Result);
                    foreach (PerformerViewModel perf in PerformerList.performers)
                    {
                        performer.name = perf.name;
                        performer.id = perf.id;

                        ResultList.Add(performer);

                    }
                }
            }
            return View("SearchResult","ArtistSearch",ResultList);
        }
        public ActionResult SearchSimilar(SearchSimilarViewModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://api.songkick.com/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string url = "3.0/events.json?apikey={}&artist_name=" + model.Search;
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {

                }
            }
                return View();
        }
    }
}