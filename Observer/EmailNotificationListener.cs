using System.Diagnostics;

namespace OnlineLibrary.Observer
{
     public class EmailNotificationListener : EventListener
     {
          public void Update(string bookId)
          {
               Debug.WriteLine($"Email notification sent for book: {bookId}");
          }
     }
}