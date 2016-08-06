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
            /*try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://192.168.2.4/webApi/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string input = @"{ ""UserName"": """ + edttxtusername.Text + @""", ""Password"":""" + edttxtpassword.Text + @""", ""ConfirmPassword"":""" + edttxtconfirmpassword.Text + @""", ""Email"":""" + edttxtemailid.Text + @""", ""PhoneNumber"":""" + edttxtmobileno.Text + @"""}";
                    HttpContent content = new StringContent(input);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // New code:
                    HttpResponseMessage response = await client.PostAsync("api/Account/Register", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetMessage("Register Successful");
                        builder.Create().Show();
                    }
                    else
                        edttxtusername.Text = response.ReasonPhrase + response.Content.ToString();// "Fail";
                }

                //if (edttxtusername.Text == "Success")
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://192.168.2.4/webApi/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string input = "username=" + edttxtusername.Text + "&password=" + edttxtpassword.Text + "&grant_type=password";
                        HttpContent content = new StringContent(input);

                        HttpResponseMessage response = await client.PostAsync("token", content);
                        if (response.IsSuccessStatusCode)
                        {
                            edttxtmobileno.Text = "Success";
                        }
                        else
                            edttxtmobileno.Text = response.ReasonPhrase + response.Content.ToString();// "Fail";
                    }
                }
            }
            catch (Exception ex)
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage(ex.Message);
                builder.Create().Show();
            }*/
            var intent = new Intent(this, typeof(MainPage));
            StartActivity(intent);



        }
    }
}