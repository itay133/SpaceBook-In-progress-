using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;


namespace SpacebookSpa.Models
{
    public class User : Person
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("Person")]
        public Person Person { get; set; }
        [BsonElement("Profileimage")]
        public String Profileimage { get; set; }
        [BsonElement("CompanyId")]
        public String CompanyId { get; set; }
        [BsonElement("Orders")]
        public List<Order> Orders { get; set; }


        ///[BsonElement("Permission")]
        // public Permission Permission
        // Need to consider if we going to add new intity for permission
    }
}