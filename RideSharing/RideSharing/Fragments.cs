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
using RideSharing.Resources;
using Android.Support.V7.Widget;
using Android.Locations;
using System.Threading.Tasks;
using RideSharing;
using System.Net;
using Android.Views.InputMethods;


//using Android.Gms.Maps;
//using Android.Gms.Maps.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RideSharing
{
    public class RidesFragment : Fragment
    {
        // RecyclerView instance that displays the photo album:
        RecyclerView mRecyclerView;

        // Layout manager that lays out each card in the RecyclerView:
        RecyclerView.LayoutManager mLayoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Rides, null);
            view.FindViewById<TextView>(Resource.Id.textViewFrom);
            view.FindViewById<TextView>(Resource.Id.textViewTo);
            view.FindViewById<TextView>(Resource.Id.textViewName);
            view.FindViewById<TextView>(Resource.Id.textViewGender);
            view.FindViewById<TextView>(Resource.Id.textViewTime);

            Rides[] rides = new Rides[] { new Rides() {Name="Ankit", From="Wakad", To="Infosys", Gender="Male", Time="8 AM" },
                new Rides() {Name="Rahil", From="Vishal Nagar", To="Infosys", Gender="Male", Time="9 AM" },
                new Rides() {Name="Abhishek", From="Hinjewadi Chowk", To="Infosys", Gender="Male", Time="9:30 AM" },
                new Rides() {Name="Rohit", From="Aundh", To="Infosys", Gender="Male", Time="8:30 AM" },
                new Rides() {Name="Rahul", From="Baner", To="Infosys", Gender="Male", Time="10:30 AM" }};

            RidesAdapter rideAD = new RidesAdapter(rides);


            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerView.SetAdapter(rideAD);

            // view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers); 


            return view;
        }
    }
    //View Holder for Rides
    public class RideViewHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView From { get; private set; }
        public TextView To { get; private set; }
        public TextView Gender { get; private set; }
        public TextView Time { get; private set; }

        public RideViewHolder(View itemview, Action<int> listner)
            : base(itemview)
        {
            Name = itemview.FindViewById<TextView>(Resource.Id.textViewName);
            From = itemview.FindViewById<TextView>(Resource.Id.textViewFrom);
            To = itemview.FindViewById<TextView>(Resource.Id.textViewTo);
            Gender = itemview.FindViewById<TextView>(Resource.Id.textViewGender);
            Time = itemview.FindViewById<TextView>(Resource.Id.textViewTime);
        }
    }

    //Adapter for Rides
    public class RidesAdapter : RecyclerView.Adapter
    {
        // Event handler for item clicks:
        public event EventHandler<int> ItemClick;

        Rides[] rides = new Rides[5];

        public override int ItemCount
        {
            get
            {
                return rides.Count();
            }
        }

        public RidesAdapter(Rides[] rides)
        {
            this.rides = rides;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.RidesCardView, parent, false);

            // Create a ViewHolder to find and hold these view references, and 
            // register OnClick with the view holder:
            RideViewHolder vh = new RideViewHolder(itemView, OnClick);
            return vh;
        }

        public override void
            OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RideViewHolder vh = holder as RideViewHolder;

            // Set the ImageView and TextView in this ViewHolder's CardView 
            // from this position in the photo album:
            vh.Name.Text = "Name : " + (this.rides[position].Name);
            vh.From.Text = "From : " + (this.rides[position].From);
            vh.To.Text = "To : " + this.rides[position].To;
            vh.Gender.Text = "Gender : " + this.rides[position].Gender;
            vh.Time.Text = "Time : " + this.rides[position].Time;
        }


        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    public class PostFragment : Fragment, ILocationListener
    {
        TextView dateDisplay;
        Button dateSelectButton;
        private TextView time_display;
        private Button pick_button;
        private int hour;
        private int minute;
        const int TIME_DIALOG_ID = 0;

        //added for autocompletetextview
        Button addressbutton;
        Location _currentLocation;
        LocationManager _locationManager;
        string _locationProvider;
        TextView _locationText;
        AutoCompleteTextView txtSearch;
        AutoCompleteTextView txtSearchFrom;
        Button PostButton;


        string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        //browser key for place webservice
        const string strGoogleApiKey = "AIzaSyC_LcvoodqYcooQlzNqNH06H-soMOYFvHU";
        const string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        //GoogleMap map;
        ArrayAdapter adapter = null;
        GeoCodeJSONClass objMapClass;
        GeoCodeJSONClass objGeoCodeJSONClass;
        string autoCompleteOptions;
        string[] strPredictiveText;
        int index = 0;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Post, null);
            //view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.Post_label);
            // RadioButton owner = view.FindViewById<RadioButton>(Resource.Id.owner);
            //RadioButton rider = view.FindViewById<RadioButton>(Resource.Id.rider);
            view.FindViewById<TextView>(Resource.Id.textViewFrom);
            //view.FindViewById<EditText>(Resource.Id.editTextFrom);
            view.FindViewById<TextView>(Resource.Id.textViewTo);
            txtSearch = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtTextSearch);
            txtSearchFrom = view.FindViewById<AutoCompleteTextView>(Resource.Id.txtTextSearchFrom);
            dateDisplay = view.FindViewById<TextView>(Resource.Id.textViewSd);
            dateSelectButton = view.FindViewById<Button>(Resource.Id.btnPickDate);
            dateSelectButton.Click += DateSelectButton_Click;
            // rider.Click += Rider_Click;
            // owner.Click += Owner_Click;
            time_display = view.FindViewById<TextView>(Resource.Id.timeDisplay);
            pick_button = view.FindViewById<Button>(Resource.Id.pickTime);
           // _addressText = view.FindViewById<TextView>(Resource.Id.address_text);
            _locationText = view.FindViewById<TextView>(Resource.Id.location_text);
            addressbutton = view.FindViewById<Button>(Resource.Id.get_address_button);
            PostButton = view.FindViewById<Button>(Resource.Id.btnPOST);
            PostButton.Click += PostButton_Click;
            addressbutton.Click += AddressButton_OnClick;

            InitializeLocationManager();

            pick_button.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);
            // Get the current time
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            // Display the current date
            UpdateDisplay();

            txtSearch.ItemClick += AutoCompleteOption_Click;
            txtSearch.Hint = "Enter Destination...";
            txtSearchFrom.ItemClick += TxtSearchFrom_ItemClick;
            txtSearchFrom.Hint = "Enter Source...";
            //********added for autocompleteview txtsearch -    start   ********
            txtSearch.TextChanged += async delegate (object sender, Android.Text.TextChangedEventArgs e)
            {
                try
                {
                    autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + txtSearch.Text + "&key=" + strGoogleApiKey);

                    if (autoCompleteOptions == "Exception")
                    {
                        Toast.MakeText(this.Activity, "Unable to connect to server!!!", ToastLength.Short).Show();
                        return;
                    }
                    objMapClass = JsonConvert.DeserializeObject<GeoCodeJSONClass>(autoCompleteOptions);
                    strPredictiveText = new string[objMapClass.predictions.Count];
                    index = 0;
                    foreach (Prediction objPred in objMapClass.predictions)
                    {
                        strPredictiveText[index] = objPred.description;
                        index++;
                    }
                    adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, strPredictiveText);
                    txtSearch.Adapter = adapter;
                }
                catch
                {
                    Toast.MakeText(this.Activity, "Unable to process at this moment!!!", ToastLength.Short).Show();
                }
                //********* added for txtsearchfrom - start ************
                txtSearchFrom.TextChanged += async delegate (object sender1, Android.Text.TextChangedEventArgs e1)
                {
                    try
                    {
                        autoCompleteOptions = await fnDownloadString(strAutoCompleteGoogleApi + txtSearchFrom.Text + "&key=" + strGoogleApiKey);

                        if (autoCompleteOptions == "Exception")
                        {
                            Toast.MakeText(this.Activity, "Unable to connect to server!!!", ToastLength.Short).Show();
                            return;
                        }
                        objMapClass = JsonConvert.DeserializeObject<GeoCodeJSONClass>(autoCompleteOptions);
                        strPredictiveText = new string[objMapClass.predictions.Count];
                        index = 0;
                        foreach (Prediction objPred in objMapClass.predictions)
                        {
                            strPredictiveText[index] = objPred.description;
                            index++;
                        }
                        adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleDropDownItem1Line, strPredictiveText);
                        txtSearchFrom.Adapter = adapter;
                    }
                    catch
                    {
                        Toast.MakeText(this.Activity, "Unable to process at this moment!!!", ToastLength.Short).Show();
                    }

                };
            };
            return view;

        }

        private async void PostButton_Click(object sender, EventArgs e)
        {
            Ride Rd = new Ride()
            {
                FROM = txtSearchFrom.Text,
                TO = txtSearch.Text,
                DATE = DateTime.Now.ToString(),
                TIME = DateTime.Now.TimeOfDay.ToString()       
            };
            var json = JsonConvert.SerializeObject(Rd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.0.102/PostAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
               // string input = @"{ 'FROM': '" + txtSearchFrom.Text + "', 'TO':'" + txtSearch.Text + "', 'DATE':'" + dateDisplay.Text + "', 'TIME':'" + time_display.Text + "'}";
               // HttpContent content = new StringContent(input, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/PostRides", content);

            }
                
        }

        async void TxtSearchFrom_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //to soft keyboard hide
            InputMethodManager inputManager = (InputMethodManager)this.Activity.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(txtSearchFrom.WindowToken, HideSoftInputFlags.NotAlways);
            //map.Clear();
            if (txtSearchFrom.Text != string.Empty)
            {
                var sb = new StringBuilder();
                sb.Append(strGeoCodingUrl);
                sb.Append("?address=").Append(txtSearchFrom.Text);
                string strResult = await fnDownloadString(sb.ToString());
                if (strResult == "Exception")
                {
                    Toast.MakeText(this.Activity, "Unable to connect to server!!!", ToastLength.Short).Show();

                }
                //below used single quote to avoid html interpretation
                objGeoCodeJSONClass = JsonConvert.DeserializeObject<GeoCodeJSONClass>(strResult);
                //LatLng Position = new LatLng(objGeoCodeJSONClass.results[0].geometry.location.lat, objGeoCodeJSONClass.results[0].geometry.location.lng);
                //updateCameraPosition(Position);
                //MarkOnMap("MyLocation", Position);
            }
        }

        async void AutoCompleteOption_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            //to soft keyboard hide
            InputMethodManager inputManager = (InputMethodManager)this.Activity.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(txtSearch.WindowToken, HideSoftInputFlags.NotAlways);
            //map.Clear();
            if (txtSearch.Text != string.Empty)
            {
                var sb = new StringBuilder();
                sb.Append(strGeoCodingUrl);
                sb.Append("?address=").Append(txtSearch.Text);
                string strResult = await fnDownloadString(sb.ToString());
                if (strResult == "Exception")
                {
                    Toast.MakeText(this.Activity, "Unable to connect to server!!!", ToastLength.Short).Show();

                }
                //below used single quote to avoid html interpretation
                objGeoCodeJSONClass = JsonConvert.DeserializeObject<GeoCodeJSONClass>(strResult);
                //LatLng Position = new LatLng(objGeoCodeJSONClass.results[0].geometry.location.lat, objGeoCodeJSONClass.results[0].geometry.location.lng);
                //updateCameraPosition(Position);
                //MarkOnMap("MyLocation", Position);
            }

        }

        async Task<string> fnDownloadString(string strUri)
        {
            WebClient webclient = new WebClient();
            string strResultData;
            try
            {
                strResultData = await webclient.DownloadStringTaskAsync(new Uri(strUri));
                Console.WriteLine(strResultData);
            }
            catch
            {
                strResultData = "Exception";
                //RunOnUiThread(() =>
                //{
                //    Toast.MakeText(this, "Unable to connect to server!!!", ToastLength.Short).Show();
                //});
            }
            finally
            {
                webclient.Dispose();
                webclient = null;
            }

            return strResultData;
        }


        //****** start - added for address picker*******//
        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)this.Activity.GetSystemService(Context.LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            //Log.Debug(TAG, "Using " + _locationProvider + ".");
        }
        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                _locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
               // commented below code to not assign value to txtsearchfrom verytime when latlong changes
                // Address address = await ReverseGeocodeCurrentLocation();
               // DisplayAddress(address);

                //Set places urlwith lat long.
                strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?location=" + _currentLocation.Latitude +","+ _currentLocation.Longitude + "&input=";
            }
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        public override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }
        public override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }
        // Updates the time we display in the TextView - time picker
        async void AddressButton_OnClick(object sender, EventArgs eventArgs)
        {
            if (_currentLocation == null)
            {
               // _addressText.Text = "Can't determine the current address. Try again in a few minutes.";
                txtSearchFrom.Text = "Loading Location...";
                return;
            }

            Address address = await ReverseGeocodeCurrentLocation();
            DisplayAddress(address);
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this.Activity);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }

        void DisplayAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
               // _addressText.Text = deviceAddress.ToString();
                txtSearchFrom.Text = deviceAddress.ToString();
            }
            else
            {
                //_addressText.Text = "Unable to determine the address. Try again in a few minutes.";
                txtSearchFrom.Text = "Loading Location....";
            }
        }
        //****** end - added for address picker*******//
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            time_display.Text = time;
        }
        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateDisplay();
        }
        protected Dialog ShowDialog1(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this.Activity, TimePickerCallback, hour, minute, false);

            return null;
        }

        protected void ShowDialog(int id)
        {
            var res = ShowDialog1(id);
            res.Show();
        }
        private void Owner_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            // Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
        }

        private void Rider_Click(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            //Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
        }

        private void DateSelectButton_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
             {
                 dateDisplay.Text = time.ToLongDateString();
             });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

        }


    }
    public class MyRidesFragment : Fragment
    {
        Button shwName;
        TextView shwText;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyRides, null);
            view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.MyRides_label);
            // view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers); 
            shwName = view.FindViewById<Button>(Resource.Id.showName);
            shwText = view.FindViewById<TextView>(Resource.Id.showText);
            shwName.Click += ShwName_Click;
            return view;

        }

        private async void ShwName_Click(object sender, EventArgs e)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://rides1.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // New code:
                    HttpResponseMessage response = await client.GetAsync("api/Rides");
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync();
                        Console.WriteLine(res.Result);
                        shwText.Text = res.Result;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}