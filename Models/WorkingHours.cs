using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class WorkingHours
    {
        [BsonElement("Day")]
        public DayOfWeek Day { get; set; }
        [BsonElement("D_Name")]
        public String D_Name { get; set; }
        [BsonElement("OpenTime")]
        public String OpenTime { get;  set; }
        [BsonElement("CloseTime")]
        public String CloseTime { get;  set; }
        [BsonElement("Selected")]
        public bool Selected { get;  set; }

    }


}
