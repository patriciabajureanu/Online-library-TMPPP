using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Singleton;

namespace OnlineLibrary.Controllers
{
     public class SingletonController : Controller
     {
          public IActionResult Index()
          {
               var db1 = DatabaseManager.Instance;
               db1.ExecuteQuery("SELECT * FROM Books");

               var db2 = DatabaseManager.Instance;
               string state = db2.GetConnectionState();

               ViewBag.SingletonDemo = $"Two instances point to same object? {ReferenceEquals(db1, db2)}. Connection state: {state}";

               return View();
          }
     }
}