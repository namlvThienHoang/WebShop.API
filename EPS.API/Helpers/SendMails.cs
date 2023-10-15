using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EPS.API.Helpers
{
    /// <summary>
    /// Thông tin của trường
    /// </summary>
    ///<modified>
    ///         Author				created date					comments
    ///         ThoNV				06/09/2019					    Tạo mới
    ///</modified>
    public class SendMails
    {
        public static void SendMail(string subject, string body, string mailto, string mailfrom, string dismailfrom, string password, string stmp = "smtp.gmail.com", int port = 587, List<Attachment> fileAtach = null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(stmp);
            mail.From = new MailAddress(mailfrom, dismailfrom);
            mail.To.Add(mailto);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            if(fileAtach!=null)
            {
                foreach (var item in fileAtach)
                {
                    mail.Attachments.Add(item);
                }
            }            
            SmtpServer.Port = port;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailfrom, password);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
