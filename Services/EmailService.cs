namespace OnlineLibrary.Services
{
     using System.Net;
     using System.Net.Mail;

     public class EmailService
     {
          public void SendEmail(string toEmail, string subject, string htmlMessage)
          {
               var fromAddress = new MailAddress("onlinelibrary.support@gmail.com", "OnlineLibrary");
               var toAddress = new MailAddress(toEmail);

               const string fromPassword = "gpbo wicg izdy lzrr";

               var smtp = new SmtpClient
               {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
               };

               var mail = new MailMessage
               {
                    From = fromAddress,
                    Subject = subject,        
                    Body = htmlMessage,      
                    IsBodyHtml = true
               };

               mail.To.Add(toAddress);

               smtp.Send(mail);
          }
     }
}