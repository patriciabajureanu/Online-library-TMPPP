namespace OnlineLibrary.Observer
{
     public class LibraryEventService
     {
          private readonly EventManager _events;

          public LibraryEventService()
          {
               _events = new EventManager();

               _events.Subscribe(new EmailNotificationListener());
               _events.Subscribe(new SmsNotificationListener());
               _events.Subscribe(new LoggingListener());
          }

          public string BorrowBook(string bookId)
          {
               _events.Notify(bookId);
               return $"Book {bookId} was borrowed successfully.";
          }

          public string ReturnBook(string bookId)
          {
               _events.Notify(bookId);
               return $"Book {bookId} was returned successfully.";
          }

          public string ReserveBook(string bookId)
          {
               _events.Notify(bookId);
               return $"Book {bookId} was reserved successfully.";
          }
     }
}