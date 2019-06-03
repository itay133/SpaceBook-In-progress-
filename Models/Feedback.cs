using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System;


namespace SpacebookSpa.Models
{
    public class Feedback
    {
        [Required]
        [BsonId]
        public ObjectId ID { get; set; }
        public ObjectId WorkSpaceID { get; set; }
        [BsonElement("User")]
        public User User { get; set; }
        [BsonElement("WeightedRate")]
        public Double WeightedRate { get; set; }
        [BsonElement("PriceRate")]
        public String PriceRate { get; set; }
        [BsonElement("AtmospherRate")]
        public String AtmospherRate { get; set; }
        [BsonElement("CleaningRate")]
        public String CleaningRate { get; set; }
        [BsonElement("ServicerRate")]
        public String ServicerRate { get; set; }
        [BsonElement("Review")]
        public String Review { get; set; }
        [BsonElement("Cons")]
        public String Cons { get; set; }
        [BsonElement("Pro")]
        public String Pro { get; set; }
    }
}