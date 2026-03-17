using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Facade.Subsystems
{
     public class UserRepository
     {
          public string GetUser(string userId)
          {
               // Returnează un nume fictiv
               return $"User_{userId}";
          }
     }
}