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

            Ride[] rides = new Ride[] { new Ride() {Name="Ankit", From="Wakad", To="Infosys" },
                new Ride() {Name="Rahil", From="Vishal Nagar", To="Infosys" },
                new Ride() {Name="Abhishek", From="Hinjewadi Chowk", To="Infosys" },
                new Ride() {Name="Rohit", From="Aundh", To="Infosys" },
                new Ride() {Name="Rahul", From="Baner", To="Infosys" }};

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

        Ride[] rides = new Ride[5];

        public override int ItemCount
        {
            get
            {
                return rides.Count();
            }
        }

        public RidesAdapter(Ride[] rides)
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
            vh.Name.Text = (this.rides[position].Name);
            vh.From.Text = (this.rides[position].From);
            vh.To.Text = this.rides[position].To;
        }


        // Raise an event when the item-click takes place:
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }

    public class PostFragment : Fragment
    {
        TextView dateDisplay;
        Button dateSelectButton;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_Post, null);
            //view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.Post_label);
            RadioButton owner = view.FindViewById<RadioButton>(Resource.Id.owner);
            RadioButton rider = view.FindViewById<RadioButton>(Resource.Id.rider);
            view.FindViewById<TextView>(Resource.Id.textViewFrom);
            view.FindViewById<EditText>(Resource.Id.editTextFrom);
            view.FindViewById<TextView>(Resource.Id.textViewTo);
            view.FindViewById<EditText>(Resource.Id.editTextTo);
            dateDisplay = view.FindViewById<TextView>(Resource.Id.textViewSd);
            dateSelectButton = view.FindViewById<Button>(Resource.Id.btnPickDate);
            dateSelectButton.Click += DateSelectButton_Click;
            rider.Click += Rider_Click;
            owner.Click += Owner_Click;
            //view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers); 
            return view;

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
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Fragment_MyRides, null);
            view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.MyRides_label);
            // view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers); 
            return view;

        }
    }
}