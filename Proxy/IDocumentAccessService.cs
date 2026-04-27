namespace OnlineLibrary.Proxy
{
     public interface IDocumentAccessService
     {
          string GetDocument(string documentId);
          string GetDocumentMetadata(string documentId);
     }
}