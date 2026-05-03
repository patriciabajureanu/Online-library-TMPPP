using System;
using System.IO;
using System.Web;

namespace OnlineLibrary.Decorator
{
     public class LoggingDecorator : BookAccessDecorator
     {
          private readonly string _userEmail;

          public LoggingDecorator(IBookAccessService inner, string userEmail) : base(inner)
          {
               _userEmail = userEmail;
          }

          public override string GetBookContent(string bookId)
          {
               var result = base.GetBookContent(bookId);

               string folderPath = HttpContext.Current.Server.MapPath("~/App_Data/Logs");

               if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

               string filePath = Path.Combine(folderPath, "book_access_log.txt");

               File.AppendAllText(
                   filePath,
                   $"[{DateTime.Now}] User: {_userEmail} accessed book ID: {bookId}. Result: {result}{Environment.NewLine}"
               );

               return result;
          }
     }
}