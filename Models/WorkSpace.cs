using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SpacebookSpa.Models
{
    public class WorkSpace
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("Email")]
        public String Email { get; set; }
        [BsonElement("Name")]
        public String Name { get; set; }
        [BsonElement("CompanyID")]
        public String CompanyID { get; set; }
        [BsonElement("Size")]
        public String Size { get; set; }
        [BsonElement("SpaceImages")]
        public List<String> SpaceImages { get; set; }
        //need to consulte about the way we will save the working hours
        [BsonElement("WorkingHours")]
        public List<WorkingHours> WorkingHours { get; set; }
        [BsonElement("UrlSite")]
        public String UrlSite { get; set; }
        [BsonElement("WhatsupUrl")]
        public String WhatsupUrl { get; set; }
        [BsonElement("FacebookUrl")]
        public String FacebookUrl { get; set; }
        [BsonElement("Address")]
        public Address Address { get; set; }
        [BsonElement("Facilites")]
        public Facilites Facilites { get; set;}
        [BsonElement("Stands")]
        public List<Stand> Stands { get; set; }
        //[BsonElement("StandTypeList")]
        //public List<Stand> StandTypeList { get; set; }
        [BsonElement("WifiSpeed")]
        public int WifiSpeed { get; set; }
        [BsonElement("Phone")]
        public String Phone { get; set; }
        [BsonElement("Feedback")]
        public List<Feedback> Feedback { get; set; }
        [BsonElement("IsApproved")]
        public bool IsApproved { get; set; }
        [BsonElement("Description")]
        public String Description { get; set; }

    }
}