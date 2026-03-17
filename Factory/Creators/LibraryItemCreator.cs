using OnlineLibrary.FactoryMethod.Interfaces;

namespace OnlineLibrary.FactoryMethod.Creators
{
     public abstract class LibraryItemCreator
     {
          public abstract ILibraryItem CreateItem(string title);
     }
}