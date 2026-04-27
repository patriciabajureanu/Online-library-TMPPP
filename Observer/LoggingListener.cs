using System.Diagnostics;

namespace OnlineLibrary.Observer
{
     public class LoggingListener : EventListener
     {
          public void Update(string bookId)
          {
               Debug.WriteLine($"Log saved for book action: {bookId}");
          }
     }
}