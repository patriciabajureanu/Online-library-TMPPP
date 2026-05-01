using System.Collections.Generic;
using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.Repositories
{
     public class BookRepository : IBookRepository
     {
          public List<Book> GetAll()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Books.ToList();
               }
          }

          public Book GetById(int id)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Books.FirstOrDefault(b => b.Id == id);
               }
          }
          public bool BorrowBook(int bookId, int userId)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var borrowing = new Borrowing
                    {
                         BookId = bookId,
                         UserId = userId,
                         BorrowDate = System.DateTime.Now,
                         DueDate = System.DateTime.Now.AddDays(14),
                         ReturnDate = null,
                         Status = "Borrowed",
                         IsReturned = false
                    };

                    db.Borrowings.Add(borrowing);
                    db.SaveChanges();

                    return true;
               }
          }
          public List<Borrowing> GetBorrowingsByUserId(int userId)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    return db.Borrowings
                             .Where(b => b.UserId == userId)
                             .ToList();
               }
          }
          public bool ReturnBook(int borrowingId)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var borrowing = db.Borrowings.FirstOrDefault(b => b.Id == borrowingId);

                    if (borrowing == null || borrowing.IsReturned)
                         return false;

                    var book = db.Books.FirstOrDefault(b => b.Id == borrowing.BookId);

                    if (book != null && book.AvailableCopies < book.TotalCopies)
                    {
                         book.AvailableCopies++;
                    }

                    borrowing.IsReturned = true;
                    borrowing.ReturnDate = System.DateTime.Now;
                    borrowing.Status = "Returned";

                    db.SaveChanges();

                    return true;
               }
          }
     }
}