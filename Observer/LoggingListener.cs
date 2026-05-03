using System;
using System.IO;
using System.Web;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.Observer
{
     public class LoggingListener : IEventListener
     {
          public void Update(string eventType, int bookId, string bookTitle, string username)
          {
               string folderPath = HttpContext.Current.Server.MapPath("~/App_Data/Logs");

               if (!Directory.Exists(folderPath))
               {
                    Directory.CreateDirectory(folderPath);
               }

               string filePath = Path.Combine(folderPath, "library-events.txt");

               string message =
                    $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} | Event: {eventType} | Book: {bookTitle} | User: {username}";

               File.AppendAllText(filePath, message + Environment.NewLine);
          }
     }
}