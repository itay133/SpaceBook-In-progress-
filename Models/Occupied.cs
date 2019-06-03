using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;


namespace SpacebookSpa.Models
{
    public class Occupied
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("OrderId")]
        public String OrderID { get; set; }
        [BsonElement("WorkSpaceID")]
        public String WorkSpaceID { get; set; }
        [BsonElement("StandType")]
        public String StandType { get; set; }
        [BsonElement("StandId")]
        public String StandID { get; set; }
        [BsonElement("CheckIn")]
        public DateTime CheckIn { get; set; }
        [BsonElement("CheckOut")]
        public DateTime CheckOut { get; set; }
    }
}