using System.Net.Http;
using System.Net.Http.Headers;

namespace SpacebookSpa.Models
{
    //MultipartFormDataStreamProvider is use for writing file content to a FileStream.
    //    The reason we create this file, CustomUploadMultipartFormProvider.cs, is because by 
    //default the saved file name look weird. Just like the image below. Some said this is mainly 
    //for security reason, which I not sure, but what I’m sure is I definitely will not use this 
    //high-security feature!
    public class CustomUploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public CustomUploadMultipartFormProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null && headers.ContentDisposition != null)
            {
                return headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"');
            }

            return base.GetLocalFileName(headers);
        }
    }
}