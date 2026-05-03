namespace OnlineLibrary.Adapter.Interfaces
{
     public interface IEBookReader
     {
          string OpenBook(string filePath);
          string GoToPage(int page);
          string CloseBook();
     }
}