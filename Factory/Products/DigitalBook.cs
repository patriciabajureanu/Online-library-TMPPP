using OnlineLibrary.FactoryMethod.Interfaces;

namespace OnlineLibrary.FactoryMethod.Products
{
     public class DigitalBook : ILibraryItem
     {
          private string Title;
          private double FileSizeMB;

          public DigitalBook(string title, double fileSizeMB)
          {
               Title = title;
               FileSizeMB = fileSizeMB;
          }

          public string GetTitle()
          {
               return Title;
          }

          public string GetDescription()
          {
               return $"Digital Book: {Title}, Size: {FileSizeMB} MB";
          }
     }
}