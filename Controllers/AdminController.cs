using System;
using System.Linq;
using System.Web.Mvc;
using OnlineLibrary.AbstractFactory;
using OnlineLibrary.Builder;
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
          public ActionResult CreateBook(string title, string author, string description, string bookType, int pages)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    // FACTORY METHOD
                    // Decide CE tip de obiect creezi (Printed / Digital / Audio)
                    var creator = LibraryItemCreatorProvider.GetCreator(bookType);

                    var libraryItem = creator.CreateItem(title, author, description, pages);
                    // BUILDER
                    // Aici se construiește obiectul Book pas cu pas (Title, Author, Type, Pages, etc.)
                    var book = libraryItem.ToBook();

                    db.Books.Add(book);
                    db.SaveChanges();

                    TempData["Success"] = $"Book '{title}' created as {bookType}!";

                    return RedirectToAction("CreateBook");
               }
          }


          


     }

}