using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLibrary.Facade.Subsystems
{
     public class NotificationService
     {
          public void SendConfirmation(string userId, string message)
          {
               Console.WriteLine($"[Notification] {userId}: {message}");
          }
     }
}