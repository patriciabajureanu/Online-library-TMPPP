using OnlineLibrary.FactoryMethod;

namespace OnlineLibrary.FactoryMethod
{
     public class DigitalBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string author, string description, int pages)
          {
               return new DigitalBook(title, author, description, pages);
          }
     }
}