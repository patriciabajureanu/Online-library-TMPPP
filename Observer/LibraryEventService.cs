namespace OnlineLibrary.Observer
{
     public class LibraryEventService
     {
          private readonly EventManager _events;

          public LibraryEventService()
          {
               _events = new EventManager();

               _events.Subscribe("borrow", new EmailNotificationListener());
               _events.Subscribe("borrow", new SmsNotificationListener());
               _events.Subscribe("borrow", new LoggingListener());

               _events.Subscribe("return", new EmailNotificationListener());
               _events.Subscribe("return", new SmsNotificationListener());
               _events.Subscribe("return", new LoggingListener());

               _events.Subscribe("reserve", new EmailNotificationListener());
               _events.Subscribe("reserve", new SmsNotificationListener());
               _events.Subscribe("reserve", new LoggingListener());
          }

          public void BorrowBook(int bookId, string bookTitle, string username)
          {
               _events.Notify("borrow", bookId, bookTitle, username);
          }

          public void ReturnBook(int bookId, string bookTitle, string username)
          {
               _events.Notify("return", bookId, bookTitle, username);
          }

          public void ReserveBook(int bookId, string bookTitle, string username)
          {
               _events.Notify("reserve", bookId, bookTitle, username);
          }
     }
}