namespace OnlineLibrary.Composite
{
     public interface LibraryComponent
     {
          void Display(int depth = 0);
          int GetTotalBooks();
     }
}