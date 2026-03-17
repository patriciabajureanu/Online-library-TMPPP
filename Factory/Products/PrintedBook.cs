using OnlineLibrary.FactoryMethod.Interfaces;

namespace OnlineLibrary.FactoryMethod.Products
{
     public class PrintedBook : ILibraryItem
     {
          private string Title;
          private string Author;

          public PrintedBook(string title, string author)
          {
               Title = title;
               Author = author;
          }

          public string GetTitle()
          {
               return Title;
          }

          public string GetDescription()
          {
               return $"Printed Book: {Title}, Author: {Author}";
          }
     }
}