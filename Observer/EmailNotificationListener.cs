using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace OnlineLibrary.Observer
{
     public class EmailNotificationListener : IEventListener
     {
          public void Update(string eventType, int bookId, string bookTitle, string username)
          {
               string smtpEmail = ConfigurationManager.AppSettings["SmtpEmail"];
               string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

               var message = new MailMessage();
               message.From = new MailAddress(smtpEmail);
               message.To.Add(smtpEmail);

               message.Subject = "📚 Online Library Notification";
               message.IsBodyHtml = true;

               message.Body = $@"
          <div style='font-family: Arial; padding:20px;'>
              <h2 style='color:#c5a992;'>Online Library</h2>

              <p>Hello,</p>

              <p>A new action was performed:</p>

              <div style='background:#f3f2ec; padding:15px; border-radius:10px;'>
                  <strong>Event:</strong> {eventType}<br/>
                  <strong>Book:</strong> {bookTitle}<br/>
                  <strong>User:</strong> {username}
              </div>

              <p style='margin-top:20px;'>Enjoy reading 📖</p>
          </div>";

               var client = new SmtpClient("smtp.gmail.com", 587)
               {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword)
               };

               client.Send(message);
          }
     }
}