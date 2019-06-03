using MongoDB.Bson;
using MongoDB.Driver;
using SpacebookSpa.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SpacebookSpa.DB
{
    public class DBManager
    {
        static MongoClient client;
        static IMongoDatabase DB;
        public static Exception UserNotFoundException { get; private set; }

        //Constractor
        static DBManager()
        {
            //========Local way DB=============//
            client = new MongoClient("mongodb://localhost:27017");
            DB = client.GetDatabase("SpaceBook");
            //========Mlab mongoDB server========//
            //client = new MongoClient("mongodb://admin:Admin12345@ds251632.mlab.com:51632/spacebook");
            //DB = client.GetDatabase("spacebook");
        }

        //*********************************User Handler**********************************************//

        #region User Handlers

        //Get User collection
        public static IMongoCollection<User> GetUsersCollection()
        {
            var collection = DB.GetCollection<User>("User");
            return collection;
        }

        //Add new User
        public static bool AddUser(User user)
        {
            bool result = false;
            User userToCheck = GetUserByEmail(user);
            try
            {
                if (userToCheck == null && user.Email != null)
                {
                    GetUsersCollection().InsertOne(user);
                    return result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex = new Exception("Error occurred while trying to add new user");
            }
            return result;

        }

        //Get user by Email
        public static User GetUser(User input)
        {
            User user = DB.GetCollection<User>("User").
                Find(model => model.Email == input.Email).FirstOrDefault();
            return user;
        }

        public static User UserVerification(User user)
        {
            // try get user by email
            // if user exist than check if the password is correct if not than return null

            User useryVar = DBManager.GetUserByEmail(user);
            if (useryVar != null && useryVar.Password == user.Password)
                return useryVar;
            return null;
        }

        public static User GetUserByEmail(User input)
        {
            try
            {
                User currUser = GetUsersCollection()
                    .Find(user => user.Email.ToUpper() == input.Email.ToUpper()).FirstOrDefault();
                return currUser;
            }
            catch (Exception ex)
            {
                //Save Error dat in Log file or DB

                return null;
            }
        }

        public static User GetUserById(User input)
        {
            try
            {
                var collection = DB.GetCollection<User>("User").Find(u => u.ID == input.ID).FirstOrDefault();
                return collection;
            }
            catch (Exception ex)
            {
                //Save Error dat in Log file or DB

                return null;
            }
        }
        //Update User details
        public static User UpdateUserDetails(FilterDefinition<User> filter, UpdateDefinition<User> update, User CurrUser)
        {

            try
            {
                var collection = DB.GetCollection<User>("User");
                collection.UpdateOne(filter, update);
                var user = GetUserByEmail(CurrUser);
                HttpContext.Current.Session["currUser"] = user;
                return user;


            }
            catch
            {

            }
            return null;
        }

        public static void EditUser(FilterDefinition<User> filter, UpdateDefinition<User> update)
        {
            try
            {
                var collection = DB.GetCollection<User>("User");
                collection.UpdateMany(filter, update);
            }
            catch { }
        }

        #endregion

        //*********************************Comapny Handler**********************************************//


        #region Comapny Handler

        public static IMongoCollection<Company> GetCompanyCollection()
        {
            var collection = DB.GetCollection<Company>("Company");
            return collection;
        }
        // try get company by email
        // if company exist than check if the password is correct if not than return null
        public static Company CompanyVerification(Company company)
        {
            Company CurrentCompany = GetCompanyByEmail(company);
            if (CurrentCompany != null && CurrentCompany.Password == company.Password)
                return CurrentCompany;
            return null;
        }

        //This method check if the company already exits in the system by company name and compnay mail
        public static bool CompanyValidation(Company input)
        {
            Company CompanyEmail = GetCompanyByEmail(input);
            Company CompnayName = GetCompanyByName(input);
            if (CompanyEmail == null && CompnayName == null)
            {
                return false;
            }
            return true;
        }

        // Adding a new Business User into DB after checking if is null
        public static bool AddCompany(Company company)
        {
            try
            {
                //Set companyName propertiy to Upper before making ant action
                company.CompanyName = company.CompanyName.ToUpper();
                bool CompanyExits = CompanyValidation(company);
                if (!CompanyExits)
                {
                    GetCompanyCollection().InsertOne(company);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static void UpdateComapnySpaces(WorkSpace workSpace)
        {
            Company company = (Company)HttpContext.Current.Session["currCompany"];
            var filter = Builders<Company>.Filter.Eq("Id", company.Id);
            var update = Builders<Company>.Update.Push("WorkSpaceId", workSpace.ID);



        }

        public static Company EditCompany(FilterDefinition<Company> filter, UpdateDefinition<Company> update, Company company)
        {
            try
            {
                var collection = GetCompanyCollection().UpdateOne(filter, update);
                var Company = GetCompanyByEmail(company);
                HttpContext.Current.Session["currCompany"] = Company;
                return Company;
            }
            catch
            { }

            return null;
        }

        public static Company GetCompanyByEmail(Company input)
        {
            try
            {
                Company company = GetCompanyCollection().
                    Find(model => model.Email.ToLower() == input.Email.ToLower()).FirstOrDefault();
                return company;
            }
            catch (Exception ex)
            {
                //save error data in log file or db
                return null;
            }
        }

        /// <summary>
        /// GetBusinessUserByName search for a business user by his name
        /// </summary>
        /// <param name="B_name"></param>
        /// <returns>BusinessUser</returns>
        /// 
        public static Company GetCompanyByName(Company input)
        {
            Company company = GetCompanyCollection().
                Find(model => model.CompanyName.ToLower() == input.CompanyName.ToLower()).FirstOrDefault();
            return company;
        }

        public static Company GetCompanyById(String companyId)
        {
            Company company = GetCompanyCollection().
                Find(model => model.Id.Equals(companyId)).FirstOrDefault();
            return company;
        }

        //public static IEnumerable<Order> GetWorkSpaceOrders(WorkSpace workSpace)
        //{
        //    Company CurrentCompany = (Company)HttpContext.Current.Session["currCompany"];
        //    IEnumerable<Order> WorkSpaceOrders;
        //    WorkSpaceOrders = DBManager.GetOrderCollection().
        //        Find(order => order.WorkspaceID = workSpace.ID);

        //}
        #endregion

        //*********************************WorksSpace Handler**********************************************//


        #region WorksSpace Handler
        public static IMongoCollection<WorkSpace> GetWorkSpaceCollection()
        {
            var collection = DB.GetCollection<WorkSpace>("WorkSpace");
            return collection;
        }

        public static List<WorkSpace> GetAllWorkSpaces()
        {
            var collection = DB.GetCollection<WorkSpace>("WorkSpace").AsQueryable<WorkSpace>();
            List<WorkSpace> AllWS = new List<WorkSpace>();

            return AllWS;


        }

        public static WorkSpace GetWorkSpaceByEmail(String email)
        {
            WorkSpace space = GetWorkSpaceCollection().
                Find(WS => WS.Email == email).FirstOrDefault();
            return space;
        }

        public static WorkSpace GetWorkSpaceByID(ObjectId id)
        {
            WorkSpace space = GetWorkSpaceCollection().
                Find(WS => WS.ID == id).FirstOrDefault();
            return space;
        }

        /// <summary>
        /// This method will recive a Workspace 
        /// First we check if ws already exits and then: insert the new ws into DB
        /// Secound insert into Company obj the workspaceID
        /// </summary>
        /// <param name="workSpace"></param>
        /// <returns>List<WorkSpace></returns>
        public static IEnumerable<WorkSpace> AddWorkSpace(WorkSpace workSpace)
        {
            List<WorkSpace> WorkSpaceDB = new List<WorkSpace>();
            try
            {
                var WS_ToCheck = GetWorkSpaceByEmail(workSpace.Email);
                if (WS_ToCheck == null)
                {
                    DBManager.GetWorkSpaceCollection().InsertOne(workSpace);
                    WorkSpace inserted = GetWorkSpaceByEmail(workSpace.Email);
                    WorkSpaceDB.Add(inserted);
                    DBManager.UpdateComapnySpaces(inserted);
                    return WorkSpaceDB;
                }
            }
            catch
            {
                //return exception to user that workspace already exits in the sys
            }
            return WorkSpaceDB;
        }

        public static void EditWorkSpace(FilterDefinition<WorkSpace> filter, UpdateDefinition<WorkSpace> update)
        {
            var collection = DB.GetCollection<WorkSpace>("WorkSpace");
            collection.UpdateOne(filter, update);

        }

        public static void DeleteWorkSpace(ObjectId workspaceId)
        {
            try
            {
                var collection = GetWorkSpaceCollection().FindOneAndDelete(Models => Models.ID == workspaceId);
            }
            catch { }
        }

        public static IEnumerable<WorkSpace> GetWorkSpace(String companyId)
        {
            List<WorkSpace> space = GetWorkSpaceCollection().
                Find(model => model.CompanyID == companyId).ToList();
            return space;
        }

        public static WorkSpace GetWorkSpaceById(String id)
        {
            List<WorkSpace> space = GetWorkSpaceCollection().
                Find(ws => ws.ID == ObjectId.Parse(id)).ToList();
            WorkSpace workSpace = new WorkSpace();
            workSpace = space[0];
            return workSpace;
        }

        public static bool SendMail(string to, string subject, string bodyHTMLMsg)
        {
            try
            {
                bodyHTMLMsg = "<div style='direction:rtl;text-align:right;'>" + bodyHTMLMsg + "</div>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(bodyHTMLMsg, null, "text/html");
                MailMessage oMsg = new MailMessage("spacebook.yvc@gmail.com", to);

                oMsg.Subject = subject;
                oMsg.AlternateViews.Add(avHtml);

                SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential("spacebook.yvc@gmail.com", "Admin12345");
                client.Send(oMsg);
                oMsg = null;
            }
            catch (Exception ex)
            {
                //DAL.ReportError("clsMail.cs", "SendMail()", ex.Message, ex.StackTrace, "To: " + to + " , Subject: " + subject);
            }

            return true;
        }

        public static IEnumerable<WorkSpace> SearchForSpaces(Search str)

        {
            try
            {
                String city = str.SearchText;
                List<WorkSpace> workSpaces = GetWorkSpaceCollection().
                    Find(ws => ws.Address.City == city).ToList();

                //var result = DB.GetCollection<WorkSpace>("WorkSpace").Find(ws => ws.Address.City == input).ToList();
                return workSpaces;

            }
            catch { }
            return null;
        }

        public static void AddFeedback(Feedback NewFeedback)
        {
            WorkSpace workSpace = DBManager.GetWorkSpaceCollection()
            .Find(WS => WS.ID == NewFeedback.WorkSpaceID).FirstOrDefault();
            var filter = Builders<WorkSpace>.Filter.Eq("ID", workSpace.ID);
            //If  workspace not contain any feedback we creatiing new List of Feedback
            if (workSpace.Feedback == null)
            {
                NewFeedback.ID = ObjectId.GenerateNewId();// creating new feedback ID
                List<Feedback> newList = new List<Feedback>();
                newList.Add(NewFeedback);
                //Update feedback List in the Workspace obj
                var update = Builders<WorkSpace>.Update.Set("Feedback", newList);
                DBManager.EditWorkSpace(filter, update);
            }
            //if the feedback list wasn't empty we will push into the list the new feedback and update workspace in DB
            else
            {
                NewFeedback.ID = ObjectId.GenerateNewId();// creating new feedback ID
                workSpace.Feedback.Add(NewFeedback);
                var update = Builders<WorkSpace>.Update.Set("Feedback", workSpace.Feedback);
                DBManager.EditWorkSpace(filter, update);
            }
        }
        #endregion

        //*********************************Stand Handler**********************************************//

        #region Stand Handler
        //Can be a better  query
        public static IEnumerable<Stand> GetWorkSpaceStandsByType(ObjectId workSpaceID, String type)
        {
            List<Stand> WSStands = new List<Stand>();
            WorkSpace workspace = GetWorkSpaceCollection().Find(ws => ws.ID == workSpaceID).FirstOrDefault();
            foreach (Stand stand in workspace.Stands)
                if (stand.Type == type)
                    WSStands.Add(stand);
            return WSStands;
        }

        public static IMongoCollection<StandType> GetStandTypesCollection()
        {
            var collection = DB.GetCollection<StandType>("StandType");
            return collection;
        }

        public static IEnumerable<Stand> GetStandsByType(Stand stand)
        {
            List<Stand> standList = new List<Stand>();
            WorkSpace WorkSpace = GetWorkSpaceById(stand.WS_ID);
            //need to fillter by stand type
            for (int i = 0; i < WorkSpace.Stands.Count; i++)
            {
                if (WorkSpace.Stands[i].Type == stand.Type)
                    standList.Add(WorkSpace.Stands[i]);
            }
            return standList;

        }

        public static IEnumerable<StandType> GetStandsType()
        {

            try
            {
                var collection = DBManager.GetUsersCollection();
                List<StandType> standTypes = DBManager.GetStandTypesCollection().Find(_ => true).ToList();
                return standTypes;
            }
            catch
            {
                return null;
            }
        }

        public static IList<WorkSpace> GetStand(Stand Stand)
        {
            IList<WorkSpace> ws = GetWorkSpaceCollection().Find(model => model.ID == Stand.WorkSpaceID && model.Stands.Any(st => st.ID == Stand.ID)).ToList();
            return ws;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stand"></param>
        public static void AddStand(Stand stand)
        {
            WorkSpace workSpace = DBManager.GetWorkSpaceCollection()
                .Find(WS => WS.ID == stand.WorkSpaceID).FirstOrDefault();
            var filter = Builders<WorkSpace>.Filter.Eq("ID", workSpace.ID);
            //If the stand list is empty in the current WorkSpace we create new Stand List
            if (workSpace.Stands == null)
            {
                /* stand.ID = ObjectId.GenerateNewId();*/// creating new stand ID
                List<Stand> newList = new List<Stand>();
                stand.Position = 0;
                for (int i = 0; i < stand.Quantity; i++)
                {
                    stand.ID = ObjectId.GenerateNewId(); /*creating new stand ID*/
                    newList.Add(stand);
                }
                //Update Stand List in the Workspace obj
                var update = Builders<WorkSpace>.Update
                    .Set("Stands", newList);
                DBManager.EditWorkSpace(filter, update);
            }
            //if Stand list was not empty then we just add into it the new stand
            else
            {
                var StandsCounter = workSpace.Stands.Count();
                stand.Position = StandsCounter;
                for (int i = 0; i < stand.Quantity; i++)// The company selects the number of positions(Quantity) it has. And in this loop we put each position separately
                {
                    stand.ID = ObjectId.GenerateNewId();/*creating new stand ID*/
                    workSpace.Stands.Add(stand);

                    var update = Builders<WorkSpace>.Update
                        .Set("Stands", workSpace.Stands);

                    DBManager.EditWorkSpace(filter, update);
                }
            }
        }
        //Need to be fixed
        public static bool SetStandQuantity(WorkSpace workSpace, int quntity)
        {

            DBManager.GetWorkSpaceByID(workSpace.ID);
            return false;

        }

        /// <summary>
        /// This method is used for checking stand avilabilty before placing a new order.
        /// the idea behind it is when we are checking if the stand is occupied but exsits in the work space we wiil allow the user the reserved the desired stand
        /// </summary>
        /// <param name="WS_ID"></param>
        /// <param name="standType"></param>
        /// <returns>bool=true/flase</returns>
        //public static bool IsStandTypeInWorkspace(String WS_ID, StandType standType)
        //{
        //    ObjectId WorkspaceId = new ObjectId(WS_ID);
        //    WorkSpace ws = GetWorkSpaceByID(WorkspaceId);
        //    foreach (Stand s in ws.Stands)
        //    {
        //        if (s.Type.ID.Equals(standType.Name))
        //        {
        //            return true;
        //        }

        //    }
        //    return false;

        //}

        #endregion End Stand Handler

        //*********************************Orders Handler**********************************************//

        #region Orders Handlers

        //get the order collection
        public static IMongoCollection<Order> GetOrderCollection()
        {
            var collection = DB.GetCollection<Order>("Orders");
            return collection;
        }

        public static IEnumerable<Order> GetAllOrder()
        {
            return DBManager.GetOrderCollection().Find(_ => true).ToList();
        }

        public static void DeleteOrder(String id)
        {
            ObjectId ID = new ObjectId(id);
            var collection = GetOrderCollection().FindOneAndDelete(Models => Models.ID == ID);
        }



        public static IEnumerable<Order> GetOrder(WorkSpace workSpace)
        {
            IEnumerable<Order> orders = DBManager.GetOrderCollection().
                Find(model => model.WorkspaceID == workSpace.ID.ToString()).ToList();
            return orders;
        }

        //get the Occupied Stands
        public static IMongoCollection<Occupied> GetOccupiedCollection()
        {
            var collection = DB.GetCollection<Occupied>("Occupied");
            return collection;
        }

        //Get from the occupied collection all matchs result (stand) as a list.
        //when user want to place an order we will get list from the occupied collection in order to check date avilability inside the occupetion list
        public static IEnumerable<Occupied> GetOccupiedStandsByType(ReservedStand Stand)
        {
            var collection = GetOccupiedCollection().Find(rs => rs.StandType == Stand.StandType && rs.WorkSpaceID == Stand.WorkSpaceID).ToList();
            return collection;

        }

        //Get all the occupied stand

        //var filter = Builders<Occupied>.Filter;
        //var SearchFilter = filter.Lte(d => d.CheckIn, Stand.CheckIn) &
        //    filter.Gt(d => d.CheckOut, Stand.CheckIn) &
        //    filter.Eq(oc => oc.StandType, Stand.StandType) & filter.Eq(oc => oc.WorkSpaceID, Stand.WorkSpaceID);
        //var collection = GetOccupiedCollection().Find(SearchFilter).ToList();

        public static IEnumerable<Occupied> GetOccuipedInPeriod(ReservedStand Stand)
        {
            //insertStandToOccupiedList(Stand);

            var filter = Builders<Occupied>.Filter;
            var SearchFilter = ((filter.Gte(d => d.CheckIn, Stand.CheckIn) &
                filter.Lt(d => d.CheckIn, Stand.CheckOut)) | (filter.Gte(d => d.CheckOut, Stand.CheckIn))) &
                filter.Eq(oc => oc.StandType, Stand.StandType) & filter.Eq(oc => oc.WorkSpaceID, Stand.WorkSpaceID);

            var collection = GetOccupiedCollection().Find(SearchFilter).ToList();

            return collection;
            //אני רוצה את כל הסטנדים שהתאריך המבוקש לא נופל בין התאריכים המבוקשים ומאחר את זה עם סטנד ליסט של אותו חלל עבודה
        }

        public static IEnumerable<Stand> CheckIfStandAvailable(IEnumerable<Occupied> occupiedStand, ReservedStand R_Stand)
        {
            ObjectId WS_ID = new ObjectId(R_Stand.WorkSpaceID);
            var WorkspaceStands = GetWorkSpaceStandsByType(WS_ID, R_Stand.StandType);
            //This query take all the exsiting stand (*By Type) in the given workspace and remove from it the occuiped stand  
            var query = from std in WorkspaceStands.AsQueryable()
                        join ocd in occupiedStand.AsQueryable() on std.ID.ToString() equals ocd.StandID into AvialableStands
                        where !AvialableStands.Any()
                        select new Stand
                        {
                            ID = std.ID,
                            WS_ID = std.WS_ID,
                            DailyRate = std.DailyRate,
                            MonthlyRate = std.MonthlyRate,
                            Type = std.Type
                        };
            IEnumerable<Stand> Avialable = query.ToList();
            return Avialable;
        }

        public static void InsertOccuiped(Occupied occupied)
        {
            try
            {
                DBManager.GetOccupiedCollection().InsertOne(occupied);
            }
            catch { }

        }

        public static void InsertOrderIntoUserOrders(Order order)
        {
            User User = (User)HttpContext.Current.Session["CurrUser"];

            var filter = Builders<User>.Filter.Eq("ID", User.ID);

            //If the order list is empty in the current user we create new order list
            if (User.Orders == null)
            {
                /* stand.ID = ObjectId.GenerateNewId();*/// creating new stand ID
                List<Order> newList = new List<Order>();
                newList.Add(order);

                //Update Orders List in the user obj
                var update = Builders<User>.Update
                    .Set("Orders", newList);
                DBManager.EditUser(filter, update);
            }
            //if orders list was not empty then we just add it into  the new orders list and update user.
            else
            {
                User.Orders.Add(order);
                var update = Builders<User>.Update
                     .Set("Orders", User.Orders);
                DBManager.EditUser(filter, update);
            }
        }

        //Add order to the DB
        public static bool CreateOrder(Order order)
        {
            //TODO: after creating a new order we need to set the stand quntity in the current WS...
            //to genrate in workspace order list the current order.
            bool response = false;
            if (order != null)
            {



                GetOrderCollection().InsertOne(order);
                var NewOrder = order;
                SetStandsAsOccuiped(NewOrder);
                InsertOrderIntoUserOrders(NewOrder);
                response = true;
            }
            return response;
        }

        //Set Stand as occuiped will set each stand in the order to be occuiped.
        //in order to prevent dupllicates
        public static bool SetStandsAsOccuiped(Order order)
        {
            try
            {
                if (order != null)
                {
                    foreach (ReservedStand RS in order.StandsInOrder)
                    {
                        Occupied o = Convert_ReservedStandToOccupied(RS, order.ID);
                        InsertOccuiped(o);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        //Convert ReservedStand object to Occuiped obj in order to set the stand to be occuiped in the date that as given in each order item.
        public static Occupied Convert_ReservedStandToOccupied(ReservedStand RS, ObjectId OrderId)
        {
            if (RS != null)
            {
                Occupied item = new Occupied()
                {
                    OrderID = OrderId.ToString(),
                    CheckIn = RS.CheckIn,
                    CheckOut = RS.CheckOut,
                    StandID = RS.StandID,
                    StandType = RS.StandType,
                    WorkSpaceID = RS.WorkSpaceID
                };
                return item;
            }
            return null;
        }


        #endregion End Orders Handler


    }
}





