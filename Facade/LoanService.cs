using OnlineLibrary.Data;
using OnlineLibrary.Models;
using System;

public class LoanService
{
     public void CreateLoan(string userId, int bookId)
     {
          using (var db = new OnlineLibraryDbContext())
          {
               var loan = new Loan
               {
                    UserEmail = userId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now
               };

               var book = db.Books.Find(bookId);
               book.AvailableCopies--;

               db.Loans.Add(loan);
               db.SaveChanges();
          }
     }
}