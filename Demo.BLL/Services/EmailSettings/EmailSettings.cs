using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Identity;

namespace Demo.BLL.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {
            //Check Spam if you didn't receive the email
            ////email is the receiver  
            ////Client is the sender?
            //var client = new SmtpClient("smtp.gmail.com", 587);
            //client.EnableSsl = true;
            ////from company email or sender go to manage Google account, then security, then 2 step verification,   
            //client.Credentials = new NetworkCredential("hassankoream@gmail.com", "nhjymykxnbxywtri");
            //client.Send("hassankoream@gmail.com", email.To, email.Subject, email.Body);

            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("hassankoream@gmail.com", "nhjymykxnbxywtri");
                    client.Send("hassankoream@gmail.com", email.To, email.Subject, email.Body);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }


        }
    }
}
