using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Facade.Subsystems
{
     public class LoanService
     {
          public string CreateLoan(string userId, string bookId)
          {
               return $"Loan_{userId}_{bookId}_{DateTime.Now:yyyyMMddHHmmss}";
          }
     }
}