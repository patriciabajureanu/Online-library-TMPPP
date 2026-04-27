namespace OnlineLibrary.Decorator
{
     public class BasicBookAccessService : IBookAccessService
     {
          public string GetBookContent(string bookId)
          {
               return $"Content of book {bookId}: This is the original content of the book.";
          }
     }
}