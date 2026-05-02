namespace OnlineLibrary.FactoryMethod
{
     public class PrintedBookCreator : LibraryItemCreator
     {
          public override ILibraryItem CreateItem(string title, string description)
          {
               return new PrintedBook(title, description);
          }
     }
}