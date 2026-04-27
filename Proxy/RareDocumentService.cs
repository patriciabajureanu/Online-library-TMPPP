namespace OnlineLibrary.Proxy
{
     public class RareDocumentService : IDocumentAccessService
     {
          public string GetDocument(string documentId)
          {
               return $"Rare archived document '{documentId}' loaded successfully.";
          }

          public string GetDocumentMetadata(string documentId)
          {
               return $"Metadata for document '{documentId}': Category = Rare Archive, Status = Restricted Access.";
          }
     }
}