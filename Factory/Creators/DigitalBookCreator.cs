using OnlineLibrary.FactoryMethod.Interfaces;
using OnlineLibrary.FactoryMethod.Products;

namespace OnlineLibrary.FactoryMethod.Creators
{
     public class DigitalBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title)
          {
               return new DigitalBook(title, 5.5);
          }
     }
}