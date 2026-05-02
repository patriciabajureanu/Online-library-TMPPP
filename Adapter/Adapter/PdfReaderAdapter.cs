using OnlineLibrary.Adapter.Adaptee;
using OnlineLibrary.Adapter.Interfaces;

namespace OnlineLibrary.Adapter.Adapters
{
     public class PdfReaderAdapter : IEBookReader
     {
          private readonly ExternalPdfReader _pdfReader;

          public PdfReaderAdapter(ExternalPdfReader pdfReader)
          {
               _pdfReader = pdfReader;
          }

          public string OpenBook(string filePath)
          {
               return _pdfReader.LoadDocument(filePath);
          }

          public string GoToPage(int page)
          {
               return _pdfReader.JumpTo(page);
          }

          public string CloseBook()
          {
               return _pdfReader.Exit();
          }
     }
}