using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Decorator;

namespace OnlineLibrary.Controllers
{
     public class DecoratorController : Controller
     {
          [HttpGet]
          public IActionResult Index()
          {
               return View("~/Views/Home/Index.cshtml");
          }

          [HttpPost]
          public IActionResult Test(string bookId)
          {
               IBookAccessService service = new BasicBookAccessService();
               service = new LoggingDecorator(service);
               service = new CachingDecorator(service);
               service = new AuthorizationDecorator(service);

               string result = service.GetBookContent(bookId);

               ViewBag.DecoratorResult = result;
               ViewBag.DecoratorInfo = "Applied decorators: Logging + Caching + Authorization";

               return View("~/Views/Home/Index.cshtml");
          }
     }
}