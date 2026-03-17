using OnlineLibrary.FactoryMethod.Interfaces;
using OnlineLibrary.FactoryMethod.Products;

namespace OnlineLibrary.FactoryMethod.Creators
{
     public class PrintedBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title)
          {
               return new PrintedBook(title, "Default Author");
          }
     }
}