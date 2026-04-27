using System.Diagnostics;

namespace OnlineLibrary.Observer
{
     public class SmsNotificationListener : EventListener
     {
          public void Update(string bookId)
          {
               Debug.WriteLine($"SMS notification sent for book: {bookId}");
          }
     }
}