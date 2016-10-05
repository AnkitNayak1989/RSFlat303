using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIPOC.Models;

namespace WebAPIPOC.Controllers
{
    public class RidesController : ApiController
    {
        [HttpPost]
        public bool PublishRide(RideModel request)
        {

            return true;
        }

    }
}
