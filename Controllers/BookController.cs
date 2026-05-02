using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.FactoryMethod;

namespace OnlineLibrary.Controllers
{
     public class BookController : Controller
     {
          [HttpPost]
          public IActionResult Create(string bookType, string title)
          {
               var creator = LibraryItemCreatorProvider.GetCreator(bookType);

               ILibraryItem book = creator.CreateItem(title, "Default description");

               ViewBag.BookDescription = book.Description;

               return View("~/Views/Home/Index.cshtml");
          }
     }
}