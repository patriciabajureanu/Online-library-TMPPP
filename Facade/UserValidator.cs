using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Facade
{ 
          public class UserValidator
          {
               public bool Validate(string userId)
               {
                    // Simplificat: valid dacă userId nu e null sau gol
                    return !string.IsNullOrEmpty(userId);
               }
          }
     
}