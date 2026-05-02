using OnlineLibrary.FactoryMethod;

namespace OnlineLibrary.FactoryMethod
{
     public class DigitalBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string description)
          {
               return new DigitalBook(title, description);
          }
     }
}