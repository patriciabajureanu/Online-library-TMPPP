namespace OnlineLibrary.Adapter.Adaptee
{
     public class ExternalPdfReader
     {
          public string LoadDocument(string path)
          {
               return "External PDF Reader loaded document: " + path;
          }

          public string JumpTo(int pageNumber)
          {
               return "External PDF Reader jumped to page: " + pageNumber;
          }

          public string Exit()
          {
               return "External PDF Reader closed the document.";
          }
     }
}