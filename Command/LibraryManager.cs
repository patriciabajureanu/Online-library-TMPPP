namespace OnlineLibrary.Command
{
     public class LibraryManager
     {
          public string BorrowBook(string bookId)
          {
               return $"Book {bookId} was borrowed.";
          }

          public string ReturnBook(string bookId)
          {
               return $"Book {bookId} was returned.";
          }

          public string ReserveBook(string bookId)
          {
               return $"Book {bookId} was reserved.";
          }
     }
}