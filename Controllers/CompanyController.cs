using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using SpacebookSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SpacebookSpa.Controllers
{
    public class CompanyController : ApiController
    {
        #region Company Handler


        [HttpGet()]
        [ActionName("GetCompanyInfo")]
        public Company GetCompanyInfo()
        {
            Company sample = new Company()
            {
                CompanyName = "comp",
            };
            Company company = DB.DBManager.GetCompanyByName(sample);
            return sample;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCompany()
        {
            HttpResponseMessage response;
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            if (CurrentCompany != null)
            {
                return response = Request.CreateResponse(HttpStatusCode.OK, CurrentCompany);
            }
            return response = Request.CreateResponse(HttpStatusCode.NotFound, CurrentCompany);
        }

        [Route("api/Company/UploadImg")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateCompnayAsync()
        {
            string path = HttpContext.Current.Server.MapPath("~/Uploads");
            var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(path);
            try
            {
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                List<KeyValuePair<string, string>> formValues = FormUtlities.ParseFormKeys(multipartFormDataStreamProvider);
                //var CompanyString = formValues.Find(el => el.Key.Equals("company")).Value;
                Company Company = (Company)HttpContext.Current.Session["currCompany"];
                List<string> paths = FormUtlities.filePaths(multipartFormDataStreamProvider);//need to insert to the db all images path
                Company.LogoUrl = paths.FirstOrDefault();
                return Request.CreateResponse( Update(Company));
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while trying to upload files!");
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Update(Company company)
        {
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            var filter = Builders<Company>.Filter.Eq("Id", CurrentCompany.Id);
            var update = Builders<Company>.Update
                .Set("CompanyName", company.CompanyName)
                .Set("ContactName", company.ContactName)
                .Set("Phone", company.Phone)
                .Set("Email", company.Email)
                .Set("Description", company.Description)
                .Set("LogoUrl",company.LogoUrl);
            DB.DBManager.EditCompany(filter, update , CurrentCompany);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Update company successfully");
            return response;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ChangePassword(Company company)
        {
            HttpResponseMessage response;
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            if (CurrentCompany.Password != company.Password)
            {
                var filter = Builders<Company>.Filter.Eq("Id", CurrentCompany.Id);
                var update = Builders<Company>.Update
                    .Set("Password", company.Password);
                DB.DBManager.EditCompany(filter, update, CurrentCompany);
                response = Request.CreateResponse(HttpStatusCode.OK, "Update Password successfully");
                return response;
            }
            response = Request.CreateResponse(HttpStatusCode.Forbidden, "Password can't be the same as the last one");
            return response;
        }

        #endregion End Company Handler


        #region WorkSpace Handler


        [System.Web.Http.HttpGet]
        public List<WorkSpace> GetWorkSpace()
        {
            List<WorkSpace> result = new List<WorkSpace>();
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];

            if (CurrentCompany != null)
            {
                result = DB.DBManager.GetWorkSpace(CurrentCompany.Id.ToString()).ToList();
                if (result.Any())
                    return result;/*workSpace*/
            }


            return result;
        }

        [System.Web.Http.HttpGet]
        public List<WorkSpace> GetAllWorkSpaces()
        {
            List<WorkSpace> AllWorkSpaces = DB.DBManager.GetAllWorkSpaces();
            return AllWorkSpaces;
        }

        [System.Web.Http.HttpPost]
        public WorkSpace GetWorkSpaceById(string workSpaceID)
        {
            WorkSpace result = new WorkSpace();
            try
            {
                if (workSpaceID != null)
                {
                    result = DB.DBManager.GetWorkSpaceById(workSpaceID);
                    if (result != null)
                        return result;/*workSpace*/
                }
            }
            catch { }
            return result;
        }

        [System.Web.Http.HttpGet]
        public List<String> GetFacilities()
        {
            List<String> facilities = new List<String>();
            foreach (string facility in Enum.GetNames(typeof(FacilitesEnum)))
            {
                facilities.Add(facility);
            }
            return facilities;
        }

        [Route("api/Company/AddWorkSpace")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> AddWorkSpaceAsync()
        {

            string path = HttpContext.Current.Server.MapPath("~/Uploads");
            var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(path);
            try
            {
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                List<KeyValuePair<string, string>> formValues = FormUtlities.ParseFormKeys(multipartFormDataStreamProvider);
                var workspaceString = formValues.Find(el => el.Key.Equals("workspace")).Value;
                WorkSpace workSpace = JsonConvert.DeserializeObject<WorkSpace>(workspaceString);
                List<string> paths = FormUtlities.filePaths(multipartFormDataStreamProvider);//need to insert to the db all images path
                workSpace.SpaceImages = paths;
                return Request.CreateResponse(HttpStatusCode.OK, AddWorkSpace(workSpace));

            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while trying to upload files!");
            }
        }




        public IEnumerable<WorkSpace> AddWorkSpace(WorkSpace newWorkSpace)
        {
            List<WorkSpace> result = new List<WorkSpace>();
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            try
            {
                if (newWorkSpace != null && CurrentCompany != null)
                {
                    newWorkSpace.CompanyID = CurrentCompany.Id.ToString();// add to new ws is relvant company id
                    DB.DBManager.AddWorkSpace(newWorkSpace);
                    result.Add(DB.DBManager.GetWorkSpaceByEmail(newWorkSpace.Email));
                }
            }
            catch { }

            return result;
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteWorkSpace(ObjectId Id)
        {
            if (Id == null)
                return BadRequest("Not a valid Id ");
            DB.DBManager.DeleteWorkSpace(Id);
            return Ok();
        }

        [System.Web.Http.HttpPost]
        public void AddFeedback(Feedback NewFeedback)
        {
            try
            {
                DB.DBManager.AddFeedback(NewFeedback);
            }
            catch
            {

            }

        }

        #endregion End of WorkSpace handler


        #region Stands Handler


        [System.Web.Http.HttpGet]
        public List<StandType> GetStandsType()
        {
            List<StandType> standTypes = DB.DBManager.GetStandsType().ToList();
            return standTypes;
        }

        [System.Web.Http.HttpGet]
        public List<Stand> GetStandsByType(Stand stand)
        {
            if (stand != null)
            {
                List<Stand> standsByType = DB.DBManager.GetStandsByType(stand).ToList();
                return standsByType;
            }
            else return null;
        }

        [Route("api/Company/AddStand")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> AddStandAsync()
        {

            string path = HttpContext.Current.Server.MapPath("~/Uploads");
            var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(path);
            try
            {
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
                List<KeyValuePair<string, string>> formValues = FormUtlities.ParseFormKeys(multipartFormDataStreamProvider);
                var workspaceString = formValues.Find(el => el.Key.Equals("stand")).Value;
                Stand Stand = JsonConvert.DeserializeObject<Stand>(workspaceString);
                List<string> paths = FormUtlities.filePaths(multipartFormDataStreamProvider);//need to insert to the db all images path
                Stand.Images= paths;
                return Request.CreateResponse(HttpStatusCode.OK,AddStand(Stand));

            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error while trying to upload files!");
            }
        }
        [System.Web.Http.HttpPost]
        public bool AddStand(Stand newStand)
        {
            List<WorkSpace> result = new List<WorkSpace>();
            ObjectId WS_ID = new ObjectId(newStand.WS_ID);
            newStand.WorkSpaceID = WS_ID;
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            try
            {
                if (newStand != null && CurrentCompany != null)
                {
                    DB.DBManager.AddStand(newStand);
                    return true;
                }
            }
            catch { }

            return false;
        }

        #endregion End of Stands Handler

    }
}









//Address ad = new Address() { City = "עיר", Street = "רחוב", Number = "10" };

//WorkSpace ws1 = new WorkSpace()
//{
//    Name = "שם המתחם",


//};
//List<WorkSpace> sample = new List<WorkSpace>();
//ws1.Address = ad;
//sample.Add(ws1);
//sample.Add(ws1);