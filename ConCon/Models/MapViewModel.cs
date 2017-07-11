using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConCon.Models
{
    public class MapViewModel
    {
        public ApplicationUser user { get; set; }
        public List<object> events { get; set; }
    }
}