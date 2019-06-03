using MongoDB.Driver;
using SpacebookSpa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace SpacebookSpa.Controllers
{

    public class UserController : ApiController
    {


        [System.Web.Http.HttpGet]
        public User GetUserProfile()
        {
            User CurrUser = (User)HttpContext.Current.Session["currUser"];
            if (CurrUser != null)
            {
                var GetUser = DB.DBManager.GetUserByEmail(CurrUser);
                return GetUser;
            }
            return null;
        }
        //this method will update the user. it is used mainly in the profile page
        //and the resson for it is beacuse we use formdata and uploding with the user data a profile picture
        [Route("api/User/ImgUpdate")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateUserAsync()
        {
            string path = HttpContext.Current.Server.MapPath("~/Uploads");
            var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(path);
            try
            {
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                List<KeyValuePair<string, string>> formValues = FormUtlities.ParseFormKeys(multipartFormDataStreamProvider);
                User User = (User)HttpContext.Current.Session["currUser"];
                List<string> paths = FormUtlities.filePaths(multipartFormDataStreamProvider);//need to insert to the db all images path
                User.Profileimage = paths.FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, Update(User));

            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while trying to upload files!");
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetUserOrders()
        {
            HttpResponseMessage response;
            User CurrUser = (User)HttpContext.Current.Session["currUser"];
            if (CurrUser != null)
            {
                User user = DB.DBManager.GetUserByEmail(CurrUser);
                response = Request.CreateResponse(HttpStatusCode.OK, user.Orders);
                return response;
            }
            return response = Request.CreateResponse(HttpStatusCode.NoContent, "Not found any data to display");
        }

        [System.Web.Http.HttpGet]
        public bool CheckUserStatus()
        {
            User CurrUser = (User)HttpContext.Current.Session["CurrUser"];

            if (CurrUser != null)
            {
                return true;//single
            }
            return false;//maried
        }
        [System.Web.Http.HttpGet]
        public void Logout()
        {
            HttpContext.Current.Session["CurrUser"] = null;

        }

        [System.Web.Http.HttpGet]
        public bool CheckEmailAdrsse(User User)
        {
            var GetUser = DB.DBManager.GetUserByEmail(User);
            if (GetUser != null)
            {
                return true;
            }
            return false;
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Update(User user)
        {
            User CurrUser = (User)HttpContext.Current.Session["currUser"];
            var filter = Builders<User>.Filter.Eq("ID", CurrUser.ID);
            var update = Builders<User>.Update
            .Set("FirstName", user.FirstName)
            .Set("LastName", user.LastName)
            .Set("Phone", user.Phone)
            .Set("Email", user.Email)
            .Set("Password", user.Password)
            .Set("Profileimage", user.Profileimage);
            DB.DBManager.UpdateUserDetails(filter, update, CurrUser);
            return Request.CreateResponse(HttpStatusCode.OK, "Update user successfully");
        }


    }
}


