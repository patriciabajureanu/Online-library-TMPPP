using OnlineLibrary.FactoryMethod.Interfaces;
using OnlineLibrary.FactoryMethod.Products;

namespace OnlineLibrary.FactoryMethod.Creators
{
     public class AudioBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title)
          {
               return new AudioBook(title, 300);
          }
     }
}