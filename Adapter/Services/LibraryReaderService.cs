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

          public string ReadBook(string filePath)
          {
               return _reader.OpenBook(filePath);
          }

          public string NavigateToPage(int page)
          {
               return _reader.GoToPage(page);
          }

          public string CloseBook()
          {
               return _reader.CloseBook();
          }
     }
}