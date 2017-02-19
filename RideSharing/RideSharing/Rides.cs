using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RideSharing
{
    public class Rides
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Time { get; set; }
    }


}