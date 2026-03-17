using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Facade.Subsystems
{
     public class AuditLogger
     {
          public void LogAction(string userId, string action)
          {
               Console.WriteLine($"[Audit] {DateTime.Now}: {userId} -> {action}");
          }
     }
}