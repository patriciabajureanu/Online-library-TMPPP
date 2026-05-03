using System;
using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.AbstractFactory;
using OnlineLibrary.Observer;

namespace OnlineLibrary.Command
{
     public class LibraryManager
     {
          public void BorrowBook(int id, string userEmail, string role)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                         return;

                    if (book.AvailableCopies <= 0)
                         return;

                    // ABSTRACT FACTORY
                    var factory = UserFactoryProvider.GetFactory(role);

                    var user = factory.CreateUser(userEmail);
                    var loanType = factory.CreateLoan(book.Title);

                    var loan = new Loan
                    {
                         BookId = book.Id,
                         UserEmail = user.GetName(),
                         UserType = user.GetUserType(),
                         BorrowDate = DateTime.Now,
                         DueDate = DateTime.Now.AddDays(loanType.GetLoanDays()),
                         ReturnDate = null,
                         IsReturned = false
                    };

                    db.Loans.Add(loan);

                    book.AvailableCopies--;

                    db.SaveChanges();

                    // OBSERVER
                    var eventService = new LibraryEventService();
                    eventService.BorrowBook(book.Id, book.Title, userEmail);
               }
          }

          public void UndoBorrowBook(int id, string userEmail)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var loan = db.Loans
                         .Where(l => l.BookId == id &&
                                     l.UserEmail == userEmail &&
                                     !l.IsReturned)
                         .OrderByDescending(l => l.BorrowDate)
                         .FirstOrDefault();

                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (loan == null || book == null)
                         return;

                    db.Loans.Remove(loan);
                    book.AvailableCopies++;

                    db.SaveChanges();
               }
          }
          public void ReturnBook(int loanId, string userEmail)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var loan = db.Loans
                         .Include("Book")
                         .FirstOrDefault(l => l.Id == loanId &&
                                              l.UserEmail == userEmail &&
                                              !l.IsReturned);

                    if (loan == null)
                         return;

                    loan.IsReturned = true;
                    loan.ReturnDate = DateTime.Now;

                    if (loan.Book != null)
                    {
                         loan.Book.AvailableCopies++;
                    }

                    db.SaveChanges();

                    var eventService = new LibraryEventService();
                    eventService.ReturnBook(loan.BookId, loan.Book.Title, userEmail);
               }
          }
          public void UndoReturnBook(int loanId, string userEmail)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var loan = db.Loans
                         .Include("Book")
                         .FirstOrDefault(l => l.Id == loanId &&
                                              l.UserEmail == userEmail &&
                                              l.IsReturned);

                    if (loan == null)
                         return;

                    loan.IsReturned = false;
                    loan.ReturnDate = null;

                    if (loan.Book != null && loan.Book.AvailableCopies > 0)
                    {
                         loan.Book.AvailableCopies--;
                    }

                    db.SaveChanges();
               }
          }

          public void ReserveBook(int bookId, string userEmail)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == bookId);

                    if (book == null)
                         return;

                    // prevenim duplicate
                    var existing = db.Reservations
                         .FirstOrDefault(r => r.BookId == bookId && r.UserEmail == userEmail);

                    if (existing != null)
                         return;

                    var reservation = new Reservation
                    {
                         BookId = bookId,
                         UserEmail = userEmail,
                         ReservedAt = DateTime.Now
                    };

                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    // OBSERVER
                    var eventService = new LibraryEventService();
                    eventService.ReserveBook(bookId, book.Title, userEmail);
               }
          }
          public void CancelReservation(int bookId, string userEmail)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var reservation = db.Reservations
                         .FirstOrDefault(r => r.BookId == bookId &&
                                              r.UserEmail == userEmail);

                    if (reservation == null)
                         return;

                    db.Reservations.Remove(reservation);
                    db.SaveChanges();
               }
          }
     }
}