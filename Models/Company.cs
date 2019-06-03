using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SpacebookSpa.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacebookSpa.Models
{
    public class Company
    {
        [Required]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        [BsonElement("CompanyName")]
        public String CompanyName { get; set; }
        [BsonElement("ContactName")]
        public String ContactName { get; set; }
        [BsonElement("Email")]
        public String Email { get; set; }
        [BsonElement("Password")]
        public String Password { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        [BsonElement("LogoUrl")]
        public String LogoUrl { get; set; }
        [BsonElement("Description")]
        public String Description { get; set; }
        [BsonElement("Spaces")]
        public List<String> WorkSpaceId { get; set; }
        [BsonElement("Phone")]
        public String Phone { get; set; }
        [BsonElement("Users")]
        public List<User> Users { get; set; }
        [BsonDateTimeOptions]
        public DateTime DateCreated { get; set; }


    }
}