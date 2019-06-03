using MongoDB.Bson;
using MongoDB.Driver;
using SpacebookSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpacebookSpa.Controllers
{
    public class SearchResultController : ApiController
    {

        [HttpPost]
        public List<WorkSpace> Search(Search SearchText)
        {
            List<WorkSpace> result = new List<WorkSpace>();
            
            try
            {
                result = DB.DBManager.SearchForSpaces(SearchText).ToList();
                return result;
            }
            catch
            {

            }
            return result;
        }


        //[System.Web.Http.HttpGet]
        //[ActionName("GetWorkSpacesInfo")]
        //public IEnumerable<Company> GetWorkSpacesInfo(string searchText)
        //{
        //    //List<Company> result = new List<Company>();
        //    //var dataFromMongo = DB.DBManager.GetCompanyCollection().Find(x => x.CompanyName == searchText).First();
        //    //result.Add(dataFromMongo);

        //    //return result;
        //    WorkSpace ws = new WorkSpace()
        //    {
        //        Name = "WsName",
        //        Phone = "053333333",
        //    };
        //    List<WorkSpace> SpaceSample = new List<WorkSpace>();
        //    SpaceSample.Add(ws);
        //    Company comp = new Company()
        //    {

        //        CompanyName = "Sample",
        //        WorkSpaceId = SpaceSample,
        //    };
        //    List<Company> companies = new List<Company>();
        //    companies.Add(comp);/* (List<Company>)DB.DBManager.GetCompanyCollection();*/
        //    return companies;
        //}



    }
}




