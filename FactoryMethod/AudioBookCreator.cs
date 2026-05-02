using OnlineLibrary.FactoryMethod;

namespace OnlineLibrary.FactoryMethod
{
     public class AudioBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string description)
          {
               return new AudioBook(title, description);
          }
     }
}