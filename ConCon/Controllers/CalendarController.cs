using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ConCon.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult GoogleAuthenticationView()
        {
            return View();
        }

    }
}