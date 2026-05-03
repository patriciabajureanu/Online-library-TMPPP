using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

public class BookCatalog
{
     public bool CheckAvailability(int bookId)
     {
          using (var db = new OnlineLibraryDbContext())
          {
               var book = db.Books.FirstOrDefault(b => b.Id == bookId);
               return book != null && book.AvailableCopies > 0;
          }
     }
     public Book GetBook(int bookId)
     {
          using (var db = new OnlineLibraryDbContext())
          {
               return db.Books.Find(bookId);
          }
     }
}