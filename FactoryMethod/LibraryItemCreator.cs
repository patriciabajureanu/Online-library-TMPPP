namespace OnlineLibrary.FactoryMethod
{
     public abstract class LibraryItemCreator
     {
          public abstract ILibraryItem CreateItem(string title, string description);
     }
}