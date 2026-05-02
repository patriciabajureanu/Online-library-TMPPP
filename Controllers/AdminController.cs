using System.Web.Mvc;
using OnlineLibrary.Data;
using OnlineLibrary.FactoryMethod;

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
     }
}