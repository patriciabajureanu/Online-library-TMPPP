using System.Web.Mvc;
using System.Web.Security;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories;

namespace OnlineLibrary.Controllers
{
     [AllowAnonymous]
     public class AccountController : Controller
     {
          [HttpGet]
          public ActionResult Register()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Register(RegisterViewModel model)
          {
               if (!ModelState.IsValid)
                    return View(model);

               IUserRepository userRepository = new UserRepository();

               var existingUser = userRepository.GetByEmail(model.Email);
               if (existingUser != null)
               {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(model);
               }

               var newUser = new User
               {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "User"
               };

               userRepository.Register(newUser);

               var savedUser = userRepository.GetByEmail(model.Email);

               FormsAuthentication.SetAuthCookie(savedUser.Username, false);
               Session["UserId"] = savedUser.Id;
               Session["Username"] = savedUser.Username;

               return RedirectToAction("DbBooks", "Home");
          }

          [HttpGet]
          public ActionResult Login()
          {
               return View();
          }

          [HttpPost]
          public ActionResult Login(LoginViewModel model)
          {
               if (!ModelState.IsValid)
                    return View(model);

               IUserRepository userRepository = new UserRepository();

               var user = userRepository.GetByEmail(model.Email);

               if (user != null && user.Password == model.Password)
               {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["UserId"] = user.Id;
                    Session["Username"] = user.Username;

                    return RedirectToAction("Index", "Home");
               }


               ModelState.AddModelError("", "Invalid email or password.");
               return View(model);
          }

          public ActionResult Logout()
          {
               FormsAuthentication.SignOut();
               Session.Clear();

               return RedirectToAction("Login", "Account");
          }
     }
}