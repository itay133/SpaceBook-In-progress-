using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SpacebookSpa.Models
{
    public class Stand
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("WorkSpaceID")]
        public ObjectId WorkSpaceID { get; set; }
        [BsonElement("Type")]
        public String Type { get; set; }
        [BsonElement("Description")]
        public String Description { get; set; }
        [BsonElement("Size")]
        public Double Size { get; set; }
        [BsonElement("Images")]
        public List<String> Images { get; set; }
        [BsonElement("DailyRate")]
        public Double DailyRate { get; set; }
        [BsonElement("MonthlRate")] 
        public Double MonthlyRate { get; set; }
        //need to consider CommitmentPeriod atribute type
        [BsonElement("CommitmentPeriod")]
        public String CommitmentPeriod { get; set; }
        [BsonElement("Availability")]
        public bool Availability { get; set; }
        [BsonElement("WS_ID")]
        public String WS_ID { get; set; }
        [BsonElement("Position")]
        public int Position { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        //[BsonElement("Person")]
        //public string Person { get; set; }
    }
}