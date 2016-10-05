using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebAPIPOC.Controllers
{
    public class LocationController : ApiController
    {
        //google api key - "AIzaSyC_LcvoodqYcooQlzNqNH06H-soMOYFvHU"


        //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&name=cruise&key=YOUR_API_KEY

        public List<string> GetSuggestion()
        {
            var response = new List<string>();
            M1();
            return response;
        }


        private async void M1()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&name=cruise&key=AIzaSyC_LcvoodqYcooQlzNqNH06H-soMOYFvHU");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=18.6079,73.76134&radius=50&types=address&key=AIzaSyC_LcvoodqYcooQlzNqNH06H-soMOYFvHU");
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync();
                    Console.WriteLine(res.Result);
                }
            }
        }
    }

    public class LocationRequest
    {
        public string LL { get; set; }
        public string Radius { get; set; }
        public string Types { get; set; }
        public string Name { get; set; }
    }
}
