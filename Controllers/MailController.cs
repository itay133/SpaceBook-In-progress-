using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Web.Security;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Data;
using SpacebookSpa.Models;
using System.Web.UI;

namespace SpacebookSpa.Controllers
{
    public class MailController : ApiController
    {
        User us = new User() { Email = "avraham973@gmail.com" };
        string sub = "hello";
        string bod = "body";

        [System.Web.Http.HttpPost]
        public void SendEmail(User sender, string subject, string bodyHTMLMs, EventArgs e)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(us.Email);
            mail.From = new MailAddress("spacebook.yvc@gmail.com", "Spacebook", System.Text.Encoding.UTF8);
            mail.Subject = "This mail is send from asp.net application";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = bod;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("spacebook.yvc@gmail.com", "admin12345");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
        }
        
        public static bool SendMail(string to, string subject, string bodyHTMLMsg)
        {
            try
            {
                bodyHTMLMsg = "<div style='direction:rtl;text-align:right;'>" + bodyHTMLMsg + "</div>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(bodyHTMLMsg, null, "text/html");
                MailMessage oMsg = new MailMessage("spacebook.yvc@gmail.com", to);

                oMsg.Subject = subject;
                oMsg.AlternateViews.Add(avHtml);

                SmtpClient client = new SmtpClient("relay-hosting.secureserver.net", 25);
                client.EnableSsl = false;
                //client.Credentials = new System.Net.NetworkCredential(smtpClient_user, smtpClient_password);
                client.Send(oMsg);
                oMsg = null;
            }
            catch (Exception ex)
            {
                //DAL.ReportError("clsMail.cs", "SendMail()", ex.Message, ex.StackTrace, "To: " + to + " , Subject: " + subject);
            }

            return true;
        }
    }
}
