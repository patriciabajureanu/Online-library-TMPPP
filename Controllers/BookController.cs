using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.FactoryMethod;
using OnlineLibrary.FactoryMethod.Creators;
using OnlineLibrary.FactoryMethod.Interfaces;

namespace OnlineLibrary.Controllers
{
     public class BookController : Controller
     {
          [HttpPost]
          public IActionResult Create(string bookType, string title)
          {
               LibraryItemCreator creator;

               switch (bookType)
               {
                    case "Printed":
                         creator = new PrintedBookCreator();
                         break;
                    case "Digital":
                         creator = new DigitalBookCreator();
                         break;
                    case "Audio":
                         creator = new AudioBookCreator();
                         break;
                    default:
                         creator = new PrintedBookCreator();
                         break;
               }

               ILibraryItem book = creator.CreateItem(title);
               ViewBag.BookDescription = book.GetDescription();

               return View("~/Views/Home/Index.cshtml");
          }
     }
}