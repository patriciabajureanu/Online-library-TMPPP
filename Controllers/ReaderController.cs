using System.Web.Mvc;
using OnlineLibrary.Adapter.Services;

namespace OnlineLibrary.Controllers
{
     public class ReaderController : Controller
     {
          private readonly LibraryReaderService _libraryService;

          public ReaderController()
          {
               _libraryService = (LibraryReaderService)System.Web.HttpContext.Current.Application["LibraryService"];
          }

          public ActionResult Read(string filePath)
          {
               _libraryService.ReadBook(filePath);
               ViewBag.AdapterResult = $"Book {filePath} opened!";
               return View("Index"); // Încarcă același view
          }

          public ActionResult GoToPage(int page)
          {
               _libraryService.NavigateToPage(page);
               ViewBag.AdapterResult = $"Navigated to page {page}";
               return View("Index");
          }

          public ActionResult Close()
          {
               _libraryService.Close();
               ViewBag.AdapterResult = "Book closed";
               return View("Index");
          }
     }
}