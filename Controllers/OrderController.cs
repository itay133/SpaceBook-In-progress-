using MongoDB.Bson;
using MongoDB.Bson.IO;
using SpacebookSpa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace SpacebookSpa.Controllers
{
    public class OrderController : ApiController
    {
        [System.Web.Http.HttpPost]
        public bool SetOrder(List<ReservedStand> items)
        {
            User CurrUser = (User)HttpContext.Current.Session["CurrUser"];

            if (CurrUser != null && items != null)
            {
                Order newOrder = new Order()
                {
                    //ID = ObjectId.GenerateNewId(),
                    DateCreated = DateTime.Now,
                    User = CurrUser,
                    StandsInOrder = items,
                    WorkspaceID = items[0].Stand.WS_ID,
                    TotalPrice = CalcOrderTotalPrice(items)
                };
                DB.DBManager.CreateOrder(newOrder);
                return true;
            }
            return false;
        }

        [System.Web.Http.HttpGet]
        public List<Order> GetCompanyOrders()
        {
            HttpResponse response;
            Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
            List<Order> orders = DB.DBManager.GetAllOrder().ToList();
            return orders;
       
        }

        [System.Web.Http.HttpPost]
        public bool DeleteCompanyOrder(object id)
        {
            string ID = id.ToString();
            
            try
            {
                DB.DBManager.DeleteOrder(ID);
                return true;
            }
            catch { }
            return false;
        }




        //This method are retrive ReservedStand obj (desire stand+dates) and check's if date therre is available stand
        //if there is avialable stand we return a list with avialable stands.If not stand has been found we returning error massage
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CheckAvilability(ReservedStand R_Stand)
        {
            R_Stand.CheckIn = R_Stand.CheckIn.AddDays(1);
            R_Stand.CheckOut = R_Stand.CheckOut.AddDays(1);
            HttpResponseMessage response;
            IEnumerable<Occupied> Occuiped = DB.DBManager.GetOccuipedInPeriod(R_Stand).ToList();  //Return all the occuiped stand for the stand type & workspace user has select
            IEnumerable<Stand> AvailableStands = DB.DBManager.CheckIfStandAvailable(Occuiped, R_Stand);
            if (AvailableStands.Count()>0 )
            {
                ReservedStand reserved = Convert_StandToReservedStand(AvailableStands.FirstOrDefault(), R_Stand);
                return response = Request.CreateResponse(HttpStatusCode.OK, reserved);
            }
            return response = Request.CreateResponse(HttpStatusCode.Conflict, "We couldn't found any avialable stand for this dates");
        }


        //Convert Stand object to ReservedStand obj 
        private ReservedStand Convert_StandToReservedStand(Stand stand, ReservedStand R_Stand)
        {
            if (stand != null && R_Stand != null)
            {
                ReservedStand reserved = new ReservedStand()
                {
                    Stand = stand,
                    StandID = stand.ID.ToString(),
                    StandType = stand.Type,
                    CheckIn = R_Stand.CheckIn,
                    CheckOut = R_Stand.CheckOut,
                    PeriodPrice = R_Stand.PeriodPrice,
                    WorkSpaceID = stand.WorkSpaceID.ToString(),
                };
                return reserved;
            }
            return null;
        }

        //Calc Order total price 
        private double CalcOrderTotalPrice(List<ReservedStand> reserveds)
        {
            double TotalPrice = 0.0;
            foreach (ReservedStand RS in reserveds)
            {
                TotalPrice += RS.PeriodPrice;
            }
            return TotalPrice;
        }

        [System.Web.Http.HttpGet]
        public List<Order> SampleOrderList()
        {
            Stand s1 = new Stand()
            {
                Size = 100,
                MonthlyRate = 220.0,
                DailyRate = 80,
                Quantity = 11,
            };
            ReservedStand item1 = new ReservedStand()
            {
                StandID = "12344",
                Stand = s1,
                CheckIn = new DateTime(),
                CheckOut = new DateTime()
            };
            User user1 = new User()
            {
                FirstName = "david",
                Email = "Avraham973@gmail.com"

            };
            ObjectId Id = new ObjectId();
            ObjectId compId = new ObjectId();
            DateTime start = new DateTime().Date;
            DateTime end = new DateTime().Date;
            List<ReservedStand> st = new List<ReservedStand>();
            st.Add(item1);
            Order order = new Order()
            {
                ID = Id,
                StandsInOrder = st,
                CompanyID = compId.ToString(),
                TotalPrice = 1000,
                User = user1

            };
            List<Order> orderList = new List<Order>();
            orderList.Add(order);
            return orderList;
        }
    }

    //[System.Web.Http.HttpGet]
    //public static IEnumerable<Order> GetComapnyOrder()
    //{

    //    Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
    //    return DB.DBManager.GetCompanyOrder(CurrentCompany);
    //}

}