using SpacebookSpa.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SpacebookSpa.Controllers
{
    public class RegistrationController : ApiController
    {

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddCompany(Company company)
        {
            HttpResponseMessage response;
            if (company != null)
            {
                company.Email = company.Email.ToUpper();
                company.DateCreated = DateTime.Now;
                bool AddingCompanySuccseed = DB.DBManager.AddCompany(company);
                if (AddingCompanySuccseed)
                {
                    HttpContext.Current.Session["currCompany"] = company;
                    response = Request.CreateResponse(System.Net.HttpStatusCode.Created, company);
                    return response;
                }
                response = Request.CreateResponse(System.Net.HttpStatusCode.Conflict, company);
                return response;
            }
            return response = Request.CreateResponse(System.Net.HttpStatusCode.NoContent, company);
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
            HttpResponseMessage response;
            if (user != null)
            {
                //user.DateCreated = DateTime.Now;
                bool AddingUserSuccseed = DB.DBManager.AddUser(user);
                if (AddingUserSuccseed)
                {
                    HttpContext.Current.Session["currUser"] = user;
                    response = Request.CreateResponse(System.Net.HttpStatusCode.Created, user);
                    return response;
                }
                response = Request.CreateResponse(System.Net.HttpStatusCode.Conflict, user);
                return response;
            }
            return response = Request.CreateResponse(System.Net.HttpStatusCode.NoContent, user);

        }
        //[System.Web.Http.HttpPost]
        //public bool AddUser(User user)
        //{

        //    bool result = DB.DBManager.AddUser(user);

        //    if (result)
        //    {
        //        HttpContext.Current.Session["currUser"] = DB.DBManager.GetUserByEmail(user);
        //        return true;
        //    }

        //    return false;
        //}
    }

}