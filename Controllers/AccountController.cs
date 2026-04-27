using System.Web.Mvc;
using System.Web.Security;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
     [AllowAnonymous]
     public class AccountController : Controller
     {
          [HttpGet]
          public ActionResult Login()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Login(LoginViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               // Demo login - pentru proiect
               if (model.Username == "admin" && model.Password == "1234")
               {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
               }

               ModelState.AddModelError("", "Invalid username or password.");
               return View(model);
          }

          public ActionResult Logout()
          {
               FormsAuthentication.SignOut();
               return RedirectToAction("Login", "Account");
          }
     }
}