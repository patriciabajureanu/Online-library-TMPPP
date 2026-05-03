using System;
using OnlineLibrary.Models;
using OnlineLibrary.Data;

namespace OnlineLibrary.Observer
{
     public class SmsNotificationListener : IEventListener
     {
          public void Update(string eventType, int bookId, string bookTitle, string username)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var notification = new Notification
                    {
                         Message = $"📱 {eventType.ToUpper()} → \"{bookTitle}\"",
                         Username = username,
                         CreatedAt = DateTime.Now,
                         IsRead = false
                    };

                    db.Notifications.Add(notification);
                    db.SaveChanges();
               }
          }
     }
}