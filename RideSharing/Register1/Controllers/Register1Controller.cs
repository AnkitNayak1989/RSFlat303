using Register1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Register1.Controllers
{
    public class Register1Controller : ApiController
    {
        public void Register(RegisterFields r)
        {
            var connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO REGISTER ([USERNAME],[PASSWORD],[MOBILE_NO],[EMAIL_ID]) VALUES ('" + r.USERNAME + "' , '" + r.PASSWORD + "','" + r.MOBILE_NO + "','" + r.EMAIL_ID + "')", conn);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    
}
