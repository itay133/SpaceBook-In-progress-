using MongoDB.Driver;
using static MongoDB.Driver.DeleteResult.Acknowledged;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shavtzak.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Shavtzak.DB
{
    public static class DBManager
    {
        static MongoClient client;
        static IMongoDatabase DB;
        static Boolean temp;
        static DBManager()
        {
            //Local DB
            client = new MongoClient("mongodb://localhost:27017");
            //Mlab DB
            //client = new MongoClient("mongodb://messi:barca1989@ds251807.mlab.com:51807/shavtzak");
            //Atlas MongoDB
            //client = new MongoClient("mongodb://ishai1987:Yvc2018!@shavtzakdb-shard-00-00-uavun.mongodb.net:27017,shavtzakdb-shard-00-01-uavun.mongodb.net:27017,shavtzakdb-shard-00-02-uavun.mongodb.net:27017/test?ssl=true&replicaSet=ShavtzakDB-shard-0&authSource=admin");
            //for all ways
            DB = client.GetDatabase("Shavtzak");

        }

        //soldiers
        public static IMongoCollection<Soldier> GetSoldierCollection()
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            return collection;
        }

        public static void AddSoldier(Soldier Soldier)
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            collection.InsertOne(Soldier);
        }

        public static void DeleteSoldier(Soldier soldier)
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            var filter = Builders<Soldier>.Filter.Eq("ID", soldier.ID);
            collection.DeleteOne(filter);
        }

        public static void EditSoldier(FilterDefinition<Soldier> filter, UpdateDefinition<Soldier> update)
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteAllSoldiers()
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            var filter = Builders<Soldier>.Filter.Empty;
            collection.DeleteMany(filter);
        }
        public static void DeleteSoldiersOfCompany(String unit, String battalion, String company)
        {
            var collection = DB.GetCollection<Soldier>("Soldier");
            var builder = Builders<Soldier>.Filter;
            var filter = builder.And(
                                builder.Eq(field: "Unit", value: unit),
                                builder.Eq(field: "Battalion", value: battalion),
                                builder.Eq(field: "Company", value: company));
            collection.DeleteMany(filter);
        }


        //Training
        public static IMongoCollection<Training> GetTrainingCollection()
        {
            var collection = DB.GetCollection<Training>("Training");
            return collection;
        }
        public static void AddTraining(Training Training)
        {
            var collection = DB.GetCollection<Training>("Training");
            collection.InsertOne(Training);
        }
        public static void DeleteTraining(Training training)
        {
            var collection = DB.GetCollection<Training>("Training");
            var filter = Builders<Training>.Filter.Eq("ID", training.ID);
            collection.DeleteOne(filter);
        }

        public static void EditTraining(FilterDefinition<Training> filter, UpdateDefinition<Training> update)
        {
            var collection = DB.GetCollection<Training>("Training");
            collection.UpdateOne(filter, update);
        }
        public static void DeleteAllTrainings()
        {
            var collection = DB.GetCollection<Training>("Training");
            var filter = Builders<Training>.Filter.Empty;
            collection.DeleteMany(filter);
        }
        public static void DeleteTrainingsOfCompany(String unit, String battalion, String company)
        {
            var collection = DB.GetCollection<Training>("Training");
            var builder = Builders<Training>.Filter;
            var filter = builder.And(
                                builder.Eq(field: "Unit", value: unit),
                                builder.Eq(field: "Battalion", value: battalion),
                                builder.Eq(field: "Company", value: company));
            collection.DeleteMany(filter);
        }


        //OperationalActivity
        public static IMongoCollection<OperationalActivity> GetOperationalActivityCollection()
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            return collection;
        }
        public static void AddOperationalActivity(OperationalActivity OperationalActivity)
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            collection.InsertOne(OperationalActivity);
        }

        public static void DeleteOperational(OperationalActivity Operational)
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            var filter = Builders<OperationalActivity>.Filter.Eq("ID", Operational.ID);
            collection.DeleteOne(filter);
        }

        public static void EditOperational(FilterDefinition<OperationalActivity> filter, UpdateDefinition<OperationalActivity> update)
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            collection.UpdateOne(filter, update);
        }
        public static void DeleteAllOp()
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            var filter = Builders<OperationalActivity>.Filter.Empty;
            collection.DeleteMany(filter);
        }
        public static void DeleteOpOfCompany(String unit, String battalion, String company)
        {
            var collection = DB.GetCollection<OperationalActivity>("OperationalActivity");
            var builder = Builders<OperationalActivity>.Filter;
            var filter = builder.And(
                                builder.Eq(field: "Unit", value: unit),
                                builder.Eq(field: "Battalion", value: battalion),
                                builder.Eq(field: "Company", value: company));
            collection.DeleteMany(filter);
        }

        ////Users
        //public static IMongoCollection<User> GetUserCollection()
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    return collection;
        //}
        //public static void AddUser(User User)
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    collection.InsertOne(User);
        //}

        //public static Boolean DeleteUser(User user)
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    var filter = Builders<User>.Filter.Eq("ID", user.ID);
        //   var a= collection.DeleteOne(filter);

        //    if (a.IsAcknowledged) {
        //        return true;
        //    }
        //    return false;
        //}

        //public static Boolean EditUser(FilterDefinition<User> filter, UpdateDefinition<User> update)
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    var a=collection.UpdateOne(filter, update);
        //    if (a.IsAcknowledged)
        //    {
        //        return true;
        //    }
        //        return false;
        //}
        //public static void DeleteAllUsers()
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    var filter = Builders<User>.Filter.Empty;
        //    collection.DeleteMany(filter);
        //}
        //public static void DeleteUsersOfFrame(String unit, String battalion, String company)
        //{
        //    var collection = DB.GetCollection<User>("User");
        //    var builder = Builders<User>.Filter;
        //    var filter = builder.And(
        //                        builder.Eq(field: "Unit", value: unit),
        //                        builder.Eq(field: "Battalion", value: battalion),
        //                        builder.Eq(field: "Company", value: company));
        //    collection.DeleteMany(filter);
        //}


        //HASHING 
        public static Boolean CheckHash(String loginPass, String DbPass)
        {
            string savedPasswordHash = DbPass;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(loginPass, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);

            int ok = 1;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    ok = 0;
                }
            }

            if (ok == 1)
            {
                return true;
            }
            return false;
        }

        public static String CreateHash(String s)
        {
            String savedPasswordHash;
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(s, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return savedPasswordHash = Convert.ToBase64String(hashBytes);
        }



        //OneSoldierInTraining
        public static IMongoCollection<OneSoldierInTraining> GetOneSoldierInTrainingCollection()
        {
            var collection = DB.GetCollection<OneSoldierInTraining>("OneSoldierInTraining");
            return collection;
        }
        public static void AddOneSoldierInTraining(OneSoldierInTraining OneSoldierInTraining)
        {
            var collection = DB.GetCollection<OneSoldierInTraining>("OneSoldierInTraining");
            collection.InsertOne(OneSoldierInTraining);
        }
        public static void EditOneSoldierInTraining(FilterDefinition<OneSoldierInTraining> filter, UpdateDefinition<OneSoldierInTraining> update)
        {
            var collection = DB.GetCollection<OneSoldierInTraining>("OneSoldierInTraining");
            collection.UpdateOne(filter, update);

        }

        //OneSoldierInOp
        public static IMongoCollection<OneSoldierInOp> GetOneSoldierInOpCollection()
        {
            var collection = DB.GetCollection<OneSoldierInOp>("OneSoldierInOp");
            return collection;
        }
        public static void AddOneSoldierInOp(OneSoldierInOp OneSoldierInOp)
        {
            var collection = DB.GetCollection<OneSoldierInOp>("OneSoldierInOp");
            collection.InsertOne(OneSoldierInOp);
        }
        public static void EditOneSoldierInOp(FilterDefinition<OneSoldierInOp> filter, UpdateDefinition<OneSoldierInOp> update)
        {
            var collection = DB.GetCollection<OneSoldierInOp>("OneSoldierInOp");
            collection.UpdateOne(filter, update);

        }


        //Msg
        public static IMongoCollection<Message> GetMessageCollection()
        {
            var collection = DB.GetCollection<Message>("Message");
            return collection;
        }
        public static void AddMessage(Message Message)
        {
            var collection = DB.GetCollection<Message>("Message");
            collection.InsertOne(Message);
        }
        public static void EditMessage(FilterDefinition<Message> filter, UpdateDefinition<Message> update)
        {
            var collection = DB.GetCollection<Message>("Message");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteMessage(Message Message)
        {
            var collection = DB.GetCollection<Message>("Message");
            var filter = Builders<Message>.Filter.Eq("ID", Message.ID);
            collection.DeleteOne(filter);
        }
        public static void DeleteAllMessage()
        {
            var collection = DB.GetCollection<Message>("Message");
            var filter = Builders<Message>.Filter.Empty;
            collection.DeleteMany(filter);
        }



        //Todo
        public static IMongoCollection<Todo> GetTodoCollection()
        {
            var collection = DB.GetCollection<Todo>("Todo");
            return collection;
        }
        public static void AddTodo(Todo Todo)
        {
            var collection = DB.GetCollection<Todo>("Todo");
            collection.InsertOne(Todo);
        }
        public static void EditTodo(FilterDefinition<Todo> filter, UpdateDefinition<Todo> update)
        {
            var collection = DB.GetCollection<Todo>("Todo");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteTodo(Todo Todo)
        {
            var collection = DB.GetCollection<Todo>("Todo");
            var filter = Builders<Todo>.Filter.Eq("ID", Todo.ID);
            collection.DeleteOne(filter);
        }


        //CompanyListAndUpdates
        public static IMongoCollection<CompanyListAndUpdates> GetCompanyListAndUpdatesCollection()
        {
            var collection = DB.GetCollection<CompanyListAndUpdates>("CompanyListAndUpdates");
            return collection;
        }
        public static void AddCompanyListAndUpdates(CompanyListAndUpdates CompanyListAndUpdates)
        {
            var collection = DB.GetCollection<CompanyListAndUpdates>("CompanyListAndUpdates");
            collection.InsertOne(CompanyListAndUpdates);
        }
        public static void EditCompanyListAndUpdates(FilterDefinition<CompanyListAndUpdates> filter, UpdateDefinition<CompanyListAndUpdates> update)
        {
            var collection = DB.GetCollection<CompanyListAndUpdates>("CompanyListAndUpdates");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteCompanyListAndUpdates(CompanyListAndUpdates CompanyListAndUpdates)
        {
            var collection = DB.GetCollection<CompanyListAndUpdates>("CompanyListAndUpdates");
            var filter = Builders<CompanyListAndUpdates>.Filter.Eq("ID", CompanyListAndUpdates.ID);
            collection.DeleteOne(filter);
        }
        public static void DeleteAllCompanyListAndUpdates()
        {
            var collection = DB.GetCollection<CompanyListAndUpdates>("CompanyListAndUpdates");
            var filter = Builders<CompanyListAndUpdates>.Filter.Empty;
            collection.DeleteMany(filter);
        }


        //Event
        public static IMongoCollection<Event> GetEventCollection()
        {
            var collection = DB.GetCollection<Event>("Event");
            return collection;
        }
        public static void AddEvent(Event Event)
        {
            var collection = DB.GetCollection<Event>("Event");
            collection.InsertOne(Event);
        }
        public static void EditEvent(FilterDefinition<Event> filter, UpdateDefinition<Event> update)
        {
            var collection = DB.GetCollection<Event>("Event");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteEvent(Event Event)
        {
            var collection = DB.GetCollection<Event>("Event");
            var filter = Builders<Event>.Filter.Eq("EventID", Event.EventID);
            collection.DeleteOne(filter);
        }
        public static void DeleteAllEvents()
        {
            var collection = DB.GetCollection<Event>("Event");
            var filter = Builders<Event>.Filter.Empty;
            collection.DeleteMany(filter);
        }
        public static void DeleteEventsOfCompany(String unit, String battalion, String company)
        {
            var collection = DB.GetCollection<Event>("Event");
            var builder = Builders<Event>.Filter;
            var filter = builder.And(
                                builder.Eq(field: "Unit", value: unit),
                                builder.Eq(field: "Battalion", value: battalion),
                                builder.Eq(field: "Company", value: company));
            collection.DeleteMany(filter);
        }



        //Admin
        public static IMongoCollection<Admin> GetAdminCollection()
        {
            var collection = DB.GetCollection<Admin>("Admin");
            return collection;
        }
        public static void AddAdmin(Admin Admin)
        {
            var collection = DB.GetCollection<Admin>("Admin");
            collection.InsertOne(Admin);
        }
        public static void EditAdmin(FilterDefinition<Admin> filter, UpdateDefinition<Admin> update)
        {
            var collection = DB.GetCollection<Admin>("Admin");
            collection.UpdateOne(filter, update);

        }
        public static void DeleteAdmin(Admin Admin)
        {
            var collection = DB.GetCollection<Admin>("Admin");
            var filter = Builders<Admin>.Filter.Eq("ID", Admin.ID);
            collection.DeleteOne(filter);
        }

        //Email
        public static IMongoCollection<Email> GetEmailCollection()
        {
            var collection = DB.GetCollection<Email>("Email");
            return collection;
        }
        public static void AddEmail(Email Email)
        {
            var collection = DB.GetCollection<Email>("Email");
            collection.InsertOne(Email);
        }

        public static void DeleteEmail(Email Email)
        {
            var collection = DB.GetCollection<Email>("Email");
            var filter = Builders<Email>.Filter.Eq("ID", Email.ID);
            collection.DeleteOne(filter);
        }

        public static void DeleteAllEmails()
        {
            var collection = DB.GetCollection<Email>("Email");
            var filter = Builders<Email>.Filter.Empty;
            collection.DeleteMany(filter);
        }


        //Vehicale
        public static IMongoCollection<Vehicale> GetVehicaleCollection()
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            return collection;
        }
        public static void AddVehicale(Vehicale Vehicale)
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            collection.InsertOne(Vehicale);
        }

        public static void DeleteVehicale(Vehicale Vehicale)
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            var filter = Builders<Vehicale>.Filter.Eq("ID", Vehicale.ID);
            collection.DeleteOne(filter);
        }

        public static void EditVehicale(FilterDefinition<Vehicale> filter, UpdateDefinition<Vehicale> update)
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            collection.UpdateOne(filter, update);
        }
        public static void DeleteAllVehicales()
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            var filter = Builders<Vehicale>.Filter.Empty;
            collection.DeleteMany(filter);
        }
        public static void DeleteVehicaleOfCompany(String code)
        {
            var collection = DB.GetCollection<Vehicale>("Vehicale");
            var builder = Builders<Vehicale>.Filter;
            var filter = builder.Eq(field: "CompanyCode", value: code);
            collection.DeleteMany(filter);
        }
    }
}