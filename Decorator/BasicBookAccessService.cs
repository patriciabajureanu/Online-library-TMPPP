using OnlineLibrary.Data;
using System.Linq;

namespace OnlineLibrary.Decorator
{
     public class BasicBookAccessService : IBookAccessService
     {
          public string GetBookContent(string bookId)
          {
               int id = int.Parse(bookId);

               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                         return "Book not found.";

                    return $"Book content loaded: {book.Title}";
               }
          }
     }
}