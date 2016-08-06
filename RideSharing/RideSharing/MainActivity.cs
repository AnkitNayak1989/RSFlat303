using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Org.Json;

namespace RideSharing
{
    [Activity(Label = "RideSharing", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        private Button btnRegister, btnLogin, btnFetchToken;
        public EditText etusername, etpassword, etemailid, etmobile;
        public EditText edttxtusername;
        public EditText edttxtpassword;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Pref_Name", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            var value1 = prefs.GetString("Key1", null);
            if (value1 != null)
            {
                var intent = new Intent(this, typeof(MainPage));
                StartActivity(intent);
            }
            else
            {
                SetContentView(Resource.Layout.LoginPage);


                btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
                btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
                btnFetchToken = FindViewById<Button>(Resource.Id.btnFetchToken);



                /*etemailid = FindViewById<EditText>(Resource.Id.edttxtEmailId);
                etusername = FindViewById<EditText>(Resource.Id.edtTxtUserName);
                etpassword = FindViewById<EditText>(Resource.Id.edtTxtPassword);
                etmobile = FindViewById<EditText>(Resource.Id.edtTxtMobile);*/

                btnRegister.Click += BtnRegister_Click;
                btnLogin.Click += BtnLogin_Click;
                btnFetchToken.Click += BtnFetchToken_Click;



            }


        }
        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            /*edttxtpassword = FindViewById<EditText>(Resource.Id.editText3);
            edttxtusername = FindViewById<EditText>(Resource.Id.editText1);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.2.4/webApi/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                string input = "username=" + edttxtusername.Text + "&password=" + edttxtpassword.Text + "&grant_type=password";
                HttpContent content = new StringContent(input);

                HttpResponseMessage response = await client.PostAsync("token", content);
                if (response.IsSuccessStatusCode)
                {
                    var builder = new AlertDialog.Builder(this);
                    //builder.SetMessage("Login Successful");
                    String AccessToken = response.Content.ReadAsStringAsync().Result.ToString();
                    JSONObject temp1 = new JSONObject(AccessToken);
                    builder.SetMessage(temp1.Get("access_token").ToString());

                    builder.Create().Show();
                    ISharedPreferences prefs = Application.Context.GetSharedPreferences("Pref_Name", FileCreationMode.Private);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.PutString("Key1", temp1.Get("access_token").ToString());
                    editor.Apply();

                }
                else
                    edttxtusername.Text = response.ReasonPhrase + response.Content.ToString();// "Fail";

            }*/
            var intent = new Intent(this, typeof(MainPage));
            StartActivity(intent);
        }

        private void BtnFetchToken_Click(object sender, EventArgs e)
        {
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("Pref_Name", FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            var value1 = prefs.GetString("Key1", "Token not found");
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(value1);
            builder.Create().Show();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // SetContentView(Resource.Layout.Register);
            var intent = new Intent(this, typeof(Register));
            StartActivity(intent);
            /*UserDetail userdetail = new UserDetail()
            {
                UserName = etusername.Text,
                EmailId = etemailid.Text,
                PhoneNumber = etmobile.Text,
                Password = etpassword.Text
            };*/

            //WebRequest req = WebRequest.Create();
            //var etusername = FindViewById<EditText>(Resource.Id.editText2);
        }
    }


    public class UserDetail
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}

