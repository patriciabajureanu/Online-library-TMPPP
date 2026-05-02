using System;
using System.Linq;
using System.Web.Mvc;
using OnlineLibrary.AbstractFactory;
using OnlineLibrary.Data;
using OnlineLibrary.FactoryMethod;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
     public class AdminController : Controller
     {
          [Authorize] // sau rol Admin dacă ai
          public ActionResult CreateBook()
          {
               return View();
          }

          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult CreateBook(string title, string description, string bookType)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    // 🔥 FACTORY METHOD
                    var creator = LibraryItemCreatorProvider.GetCreator(bookType);

                    var libraryItem = creator.CreateItem(title, description);

                    var book = libraryItem.ToBook();

                    db.Books.Add(book);
                    db.SaveChanges();

                    TempData["Success"] = $"Book '{title}' created as {bookType}!";

                    return RedirectToAction("CreateBook");
               }
          }
          [Authorize]
          public ActionResult CreateUserLoan()
          {
               return View();
          }

          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult CreateUserLoan(string userType, string name, string bookTitle)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    // 🔥 ABSTRACT FACTORY
                    var factory = UserFactoryProvider.GetFactory(userType);

                    var user = factory.CreateUser(name);
                    var loanType = factory.CreateLoan(bookTitle);

                    // 🔍 găsește cartea în DB
                    var book = db.Books.FirstOrDefault(b => b.Title == bookTitle);

                    if (book == null)
                    {
                         TempData["Error"] = "Book not found.";
                         return RedirectToAction("CreateUserLoan");
                    }

                    if (book.AvailableCopies <= 0)
                    {
                         TempData["Error"] = "No copies available.";
                         return RedirectToAction("CreateUserLoan");
                    }

                    // 🧠 creezi Loan REAL
                    var loan = new Loan
                    {
                         BookId = book.Id,
                         UserEmail = user.GetName(), // sau User.Identity.Name dacă vrei
                         UserType = user.GetUserType(),
                         BorrowDate = DateTime.Now,
                         DueDate = DateTime.Now.AddDays(loanType.GetLoanDays()),
                         ReturnDate = null,
                         IsReturned = false
                    };

                    db.Loans.Add(loan);

                    book.AvailableCopies--;

                    db.SaveChanges();

                    TempData["Success"] = $"{user.GetUserType()} loan created successfully!";

                    return RedirectToAction("CreateUserLoan");
               }
          }
     }

}