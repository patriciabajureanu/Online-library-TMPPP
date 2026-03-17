using OnlineLibrary.Adapter.Interfaces;
using OnlineLibrary.Adapter.Adaptee;

namespace OnlineLibrary.Adapter.Adapters
{
     public class PdfReaderAdapter : IEBookReader
     {
          private readonly ExternalPdfReader _pdfReader;

          public PdfReaderAdapter(ExternalPdfReader pdfReader)
          {
               _pdfReader = pdfReader;
          }

          public void OpenBook(string filePath)
          {
               _pdfReader.LoadDocument(filePath);
          }

          public void GoToPage(int page)
          {
               _pdfReader.JumpTo(page);
          }

          public void CloseBook()
          {
               _pdfReader.Exit();
          }
     }
}