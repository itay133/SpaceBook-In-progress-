using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class Person
    {
        //[Required]
        //[BsonId]
        //public ObjectId ID { get; set; }
        [BsonElement("FirstName")]
        public String FirstName { get; set; }
        [BsonElement("LastName")]
        public String LastName { get; set; }
        [BsonElement("Phone")]
        public String Phone { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Password")]
        public String Password { get; set; }
        //[BsonElement("City")]
        //public String City { get; set; }


    }
}