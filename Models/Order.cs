using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpacebookSpa.Models
{
    public class Order
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("User")]
        public User User { get; set; }
        [BsonElement("CompanyID")]
        public String CompanyID { get; set; }
        [BsonElement("WorkspaceID")]
        public String WorkspaceID { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateCreated { get; set; }
        [BsonElement("TotalPrice")]
        public double TotalPrice { get; set; }
        [BsonElement("StandsInOrder")]
        public List<ReservedStand> StandsInOrder { get; set; }
    }
}
//    [BsonElement("CompanyID")]
//    public String CompanyID { get; set; }
//    [BsonElement("TotalPrice")]
//    public long TotalPrice { get; set; }
//    [BsonElement("StartDate")]
//    public DateTime StartDate { get; set; }
//    [BsonElement("EndDate")]
//    public DateTime EndDate { get; set; }