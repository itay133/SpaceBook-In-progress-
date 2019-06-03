using SpacebookSpa.DB;
using SpacebookSpa.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
/// <summary>
/// אם החברה לא הצליחה להתחבר למערכת אנחנו צריכים להחזיר הודעת שגיאה
/// </summary>
namespace SpaceBook.Controllers
{
    public class LoginController : ApiController
    {
        // GET: Login
        [System.Web.Http.HttpPost]
        public HttpResponseMessage U_Login(User user)
        {
            HttpResponseMessage response = null;
            if (user != null)
            {
                User User = DBManager.UserVerification(user);
                if (User != null)
                {
                    HttpContext.Current.Session["currUser"] = User;
                    return response = Request.CreateResponse(HttpStatusCode.OK, User);

                }
                return response = Request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation, User);

            }
            return response = Request.CreateResponse(System.Net.HttpStatusCode.NoContent, User);

        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Login(Company company)
        {
            HttpResponseMessage response = null;
            if (company != null)
            {
                company.Email = company.Email.ToUpper();
                Company currentCompany = DBManager.CompanyVerification(company);
                if (currentCompany != null)
                {
                    HttpContext.Current.Session["currCompany"] = currentCompany;
                    return response = Request.CreateResponse(System.Net.HttpStatusCode.OK, currentCompany);

                }
                return response = Request.CreateResponse(System.Net.HttpStatusCode.NonAuthoritativeInformation, currentCompany);

            }
            return Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
            
        }

    }

}




