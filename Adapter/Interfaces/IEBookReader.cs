using System;

namespace OnlineLibrary.Adapter.Interfaces
{
     public interface IEBookReader
     {
          void OpenBook(string filePath);
          void GoToPage(int page);
          void CloseBook();
     }
}