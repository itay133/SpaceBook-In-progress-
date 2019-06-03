using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class StandType
    {
        [BsonId]
        public ObjectId MongoId { get; set; }
        [BsonElement("ID")]
        public int ID { get; set; }
        [BsonElement("Name")]
        public String Name { get; set; }
    
        

    }


}