using Post.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Post.Controllers
{
    public class PostRidesController : ApiController
    {
        public void Post(Ride ride)
        {
            var connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO POST ([FROM],[TO],[DATE],[TIME]) VALUES ('"+ride.FROM+ "' , '" + ride.TO + "','"+DateTime.Now+"','"+DateTime.Now+"')", conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
        
    }
}
