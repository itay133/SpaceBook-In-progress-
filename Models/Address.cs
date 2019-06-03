using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class Address
    {
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("Street")]
        public string Street { get; set; }
        [BsonElement("Number")]
        public string Number { get; set; }
        [BsonElement("Floor")]
        public string Floor { get; set; }

    }
}