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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace RideSharing
{
    [Activity(Label = "Register")]
    public class Register : Activity
    {
        private Button btnRegisterComplete;
        private EditText edttxtusername;
        private EditText edttxtpassword;
        private EditText edttxtconfirmpassword;
        private EditText edttxtemailid;
        private EditText edttxtmobileno;
        class Program
        {
            static void Main(string[] args)
            {

                Program pr = new Program();
                pr.method1();
                //Console.ReadLine();

            }

            public async void method1()
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:61599/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // New code:
                    HttpResponseMessage response = await client.GetAsync("api/RideSharing");
                    if (response.IsSuccessStatusCode)
                    {
                        var res = response.Content.ReadAsStringAsync();
                        Console.WriteLine(res.Result);
                    }
                }
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)

        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            btnRegisterComplete = FindViewById<Button>(Resource.Id.btnRegisterComplete);

            btnRegisterComplete.Click += BtnRegisterComplete_Click;
            // Create your application here
        }

        private async void BtnRegisterComplete_Click(object sender, EventArgs e)
        {
            edttxtusername = FindViewById<EditText>(Resource.Id.editText1);
            edttxtpassword = FindViewById<EditText>(Resource.Id.editText2);
            edttxtconfirmpassword = FindViewById<EditText>(Resource.Id.editText5);
            edttxtemailid = FindViewById<EditText>(Resource.Id.editText4);
            edttxtmobileno = FindViewById<EditText>(Resource.Id.editText3);
            try
            {
              {
                    RegisterFields r = new RegisterFields()
                    {
                        UserName = edttxtusername.Text,
                        Password = edttxtpassword.Text,
                        ConfirmPassword = edttxtconfirmpassword.Text,
                        PhoneNumber = edttxtmobileno.Text,
                        Email = edttxtemailid.Text 
                        /*UserName = "ankit",
                        Password = "ankit@123",
                        ConfirmPassword = "ankit@123",
                        PhoneNumber = "9052682419",
                        Email = "ankit@gmail.com"*/
                    };
                    var json = JsonConvert.SerializeObject(r);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://register1.azurewebsites.net/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //string input = "USERNAME=" + edttxtusername.Text + "PASSWORD=" + edttxtpassword.Text + "MOBILE_NO=" + edttxtmobileno.Text + "EMAIL_ID=" + edttxtemailid.Text;
                        //HttpContent content = new StringContent(input);

                          var res = client.PostAsync("api/Account/Register", content);
                       var r1 = res.Result;
                   /* var client = new HttpClient();
                    client.BaseAddress = new Uri("http://lift1989.azurewebsites.net/");

                    var jsonData = JsonConvert.SerializeObject(r);
                   // var content1 = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.PostAsync("api/Account/Register", content);
                    var res = response.Result; */
                    /*if (response.IsSuccessStatusCode)
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetMessage("Register Successful");
                        builder.Create().Show();
                    }*/
                    /*if (response.IsSuccessStatusCode)
                    {
                        edttxtmobileno.Text = "Success";
                    }
                    else
                        edttxtmobileno.Text = response.ReasonPhrase + response.Content.ToString();// "Fail"; */
                    }
                }
            }
            catch (Exception ex)
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage(ex.Message);
                builder.Create().Show();
            }
            var intent = new Intent(this, typeof(MainPage));
            StartActivity(intent);



        }
    }
}