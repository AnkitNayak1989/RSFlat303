using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class RidesController : ApiController
    {
        public List<String> GetRides()
        {
            List<String> var1= new List<string>();

            var connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //var connString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = RideSharing; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM AspNetUsers", conn);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var1.Add(reader.GetValue(1).ToString());
                    }
                  
                }
            }
            return var1;
        }
    }
}
