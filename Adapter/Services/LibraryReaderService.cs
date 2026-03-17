using OnlineLibrary.Adapter.Interfaces;

namespace OnlineLibrary.Adapter.Services
{
     public class LibraryReaderService
     {
          private readonly IEBookReader _reader;

          public LibraryReaderService(IEBookReader reader)
          {
               _reader = reader;
          }

          public void ReadBook(string filePath)
          {
               _reader.OpenBook(filePath);
          }

          public void NavigateToPage(int page)
          {
               _reader.GoToPage(page);
          }

          public void Close()
          {
               _reader.CloseBook();
          }
     }
}