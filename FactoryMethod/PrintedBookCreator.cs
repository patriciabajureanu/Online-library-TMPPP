namespace OnlineLibrary.FactoryMethod
{
     public class PrintedBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string author, string description, int pages)
          {
               return new PrintedBook(title, author, description, pages);
          }
     }
}