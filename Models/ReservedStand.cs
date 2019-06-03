using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpacebookSpa.Models
{
    public class ReservedStand
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("Stand")]
        public Stand Stand { get; set; }
        [BsonElement("StandID")]
        public String StandID { get; set; }
        [BsonElement("StandType")]
        public String StandType { get; set; }
        [BsonElement("WorkSpaceID")]
        public String WorkSpaceID { get; set; }
        [BsonDateTimeOptions]
        public DateTime CheckIn { get; set; }
        [BsonDateTimeOptions]
        public DateTime CheckOut { get; set; }
        [BsonElement("PeriodPrice")]
        public long PeriodPrice { get; set; }

    }
}
