using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpacebookSpa.Models
{
    public class FormUtlities
    {
        //split each element that has been insert- in this case we split each Img that has been upload and workspce obj
        public static List<KeyValuePair<string,string>> ParseFormKeys(CustomUploadMultipartFormProvider provider)
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (var key in provider.FormData.AllKeys)
            {
                foreach (var val in provider.FormData.GetValues(key))
                {
                    list.Add(new KeyValuePair<string, string>(key, val));
                }
            }

            return list;
        }
        //Passing all list file that has uploded to the server
        public static List<string> filePaths(CustomUploadMultipartFormProvider provider)
        {
            List<string> paths = new List<string>();
            foreach(System.Net.Http.MultipartFileData file in provider.FileData)
            {
                var fileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
                paths.Add("/Uploads/" + fileName);
            }

            return paths;
        }

    }



  
}