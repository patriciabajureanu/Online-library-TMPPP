using System;

namespace OnlineLibrary.Decorator
{
     public class LoggingDecorator : BookAccessDecorator
     {
          public LoggingDecorator(IBookAccessService inner) : base(inner)
          {
          }

          public override string GetBookContent(string bookId)
          {
               Console.WriteLine($"[LOG] Book accessed: {bookId} at {DateTime.Now}");
               return base.GetBookContent(bookId);
          }
     }
}