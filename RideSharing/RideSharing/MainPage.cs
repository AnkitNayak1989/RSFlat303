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
//using Android.Support.V7.App;


namespace RideSharing
{
    [Activity(Label = "MainPage")]
    public class MainPage : Activity
    {
        //private Button btnRides, btnMyRides, btnPost;
        Fragment[] _fragments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainPage);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.MainPage);
            _fragments = new Fragment[]
            {
                 new RidesFragment(),
                 new PostFragment(),
                 new MyRidesFragment()
            };

            AddTabToActionBar(Resource.String.Rides_label);
            AddTabToActionBar(Resource.String.Post_label);
            AddTabToActionBar(Resource.String.MyRides_label);
        }

        public void AddTabToActionBar(int labelresourceid)
        {
            ActionBar.Tab tab = ActionBar.NewTab().SetText(labelresourceid);

            tab.TabSelected += Tab_TabSelected;
            ActionBar.AddTab(tab);

        }

        private void Tab_TabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
        {
            ActionBar.Tab tab = (ActionBar.Tab)sender;


            Fragment frag = _fragments[tab.Position];
            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);

        }


        /*btnRides = FindViewById<Button>(Resource.Id.btnRides);
        btnPost = FindViewById<Button>(Resource.Id.btnPost);
        btnMyRides = FindViewById<Button>(Resource.Id.btnMyRides);

        btnRides.Click += BtnRides_Click;
        btnPost.Click += BtnPost_Click;
        btnMyRides.Click += BtnMyRides_Click;

    }

    private void BtnMyRides_Click(object sender, EventArgs e)
    {
        var intent = new Intent(this, typeof(Myrides));
        StartActivity(intent);
    }

    private void BtnPost_Click(object sender, EventArgs e)
    {
        var intent = new Intent(this, typeof(Post));
        StartActivity(intent);
    }

    private void BtnRides_Click(object sender, EventArgs e)
    {
        var intent = new Intent(this, typeof(Rides));
        StartActivity(intent);
    }*/
    }

}