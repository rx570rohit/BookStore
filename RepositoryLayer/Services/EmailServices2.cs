using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailServices2
    {
        public static void SendEmail(string Email, string token)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("soni1rohit570@gmail.com", "oqoerwzdrrjaexxe");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(Email);
                msgObj.From = new MailAddress("soni1rohit570@gmail.com");
                msgObj.Subject = "Welcome To Rock BookStore";
                msgObj.Body = $"www.BookStore.com/Order Information /{token}";
                smtpClient.Send(msgObj);
            }
        }
    }
}
