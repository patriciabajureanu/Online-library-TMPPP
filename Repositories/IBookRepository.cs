using System.Collections.Generic;
using OnlineLibrary.Models;

namespace OnlineLibrary.Repositories
{
     public interface IBookRepository
     {
          List<Book> GetAll();
          Book GetById(int id);
          bool BorrowBook(int bookId, int userId);
          List<Borrowing> GetBorrowingsByUserId(int userId);
          bool ReturnBook(int borrowingId);

     }

}