using ConCon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult SearchResult(ArtistSearchViewModel model)
        {
            return View();
        }
    }
}