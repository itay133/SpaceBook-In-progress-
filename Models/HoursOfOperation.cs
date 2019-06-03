using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpacebookSpa.Models;

namespace SpacebookSpa.Models
{
    public class HoursOfOperation
    {
        public List<WorkingHours> OperationHours { get; set; }
        
        //Constructor
        public HoursOfOperation()
        {
            OperationHours = new List<WorkingHours>();
        }
        //
        public void Add(WorkingHours newOpsHours)
        {
            if (newOpsHours == null) return;

            OperationHours.Add(newOpsHours);
        }


    }
}