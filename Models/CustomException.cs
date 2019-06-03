using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class CustomException: HttpException
    {
        //TODO: Check which exception should be used
        HttpException exe = new HttpException();
        
        public static string loginFailed { get { return "Email or Password incorrect"; } }
        public static string registrationFailed { get { return "somthing went wrong..."; } }
        public static string OrderFailed { get { return "somthing went wrong..."; } }
        public static string AddingStandFailed { get { return "הוספת עמדה נכשלה"; } }


    }
}