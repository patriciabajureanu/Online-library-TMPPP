namespace OnlineLibrary.FactoryMethod
{
     public class AudioBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string author, string description, int pages)
          {
               return new AudioBook(title, author, description, pages);
          }
     }
}