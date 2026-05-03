namespace OnlineLibrary.Composite
{
     public interface ILibraryComponent
     {
          string Display(int depth = 0);
          int GetTotalBooks();
     }
}