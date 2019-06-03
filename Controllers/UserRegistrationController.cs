using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using System.Web.Http;
using SpacebookSpa.Models;
using System.Web.Helpers;
using System.Collections;

namespace SpacebookSpa.Controllers
{
    public class UserRegistrationController : ApiController
    {
        [System.Web.Http.HttpPost]
        public IEnumerable<string> AddUser(User user)
        {
            List<string> response = new List<string>();
            bool result = DB.DBManager.AddUser(user);

            if(result)
            {
                HttpContext.Current.Session["currUser"] = DB.DBManager.GetUserByEmail(user);
                response.Add("ok");
            }
            else
            {
                response.Add("false");
            }
            return response;
        }






    }
}