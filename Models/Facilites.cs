using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpacebookSpa.Models;

namespace SpacebookSpa.Models
{
    public class Facilites

    {
        
        //Facilitis services:
        [BsonElement("PrintingServices")]
        public bool PrintingServices { get; set; }
        [BsonElement("Wifi")]
        public bool Wifi { get; set; }
        [BsonElement("Staff")]
        public bool Staff { get; set; }
        [BsonElement("Mail")]
        public bool Mail { get; set; }
        [BsonElement("Locker")]
        public bool Locker { get; set; }
        [BsonElement("RestingRoom")]
        public bool RestingRoom { get; set; }
        [BsonElement("Showers")]
        public bool Showers { get; set; }
        [BsonElement("Gym")]
        public bool Gym { get; set; }
        [BsonElement("SwimmingPool")]
        public bool SwimmingPool { get; set; } 
        [BsonElement("AnimalsAllowed")]
        public bool AnimalsAllowed { get; set; }
        [BsonElement("TransportationArea")]
        public bool TransportationArea { get; set; }
        [BsonElement("Parking")]
        public bool Parking { get; set; }
        [BsonElement("BicycleRoom")]
        public bool BicycleRoom { get; set; }
        [BsonElement("GameConsoles")]
        public bool GameConsoles { get; set; }
        [BsonElement("CommitmentPeriod")]
        public bool CommitmentPeriod { get; set; }
        [BsonElement("PaulTable")]
        public bool PaulTable { get; set; }
    }
}