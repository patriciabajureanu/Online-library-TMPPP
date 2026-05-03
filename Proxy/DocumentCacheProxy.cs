using System.Collections.Generic;

namespace OnlineLibrary.Proxy
{
     public class DocumentCacheProxy : IDocumentAccessService
     {
          private readonly IDocumentAccessService _realService;

          private static readonly Dictionary<string, string> _documentCache =
              new Dictionary<string, string>();

          private static readonly Dictionary<string, string> _metadataCache =
              new Dictionary<string, string>();

          public DocumentCacheProxy(IDocumentAccessService realService)
          {
               _realService = realService;
          }

          public string GetDocument(string documentId)
          {
               if (_documentCache.ContainsKey(documentId))
                    return "[CACHE] " + _documentCache[documentId];

               var document = _realService.GetDocument(documentId);
               _documentCache[documentId] = document;

               return document;
          }

          public string GetDocumentMetadata(string documentId)
          {
               if (_metadataCache.ContainsKey(documentId))
                    return "[CACHE] " + _metadataCache[documentId];

               var metadata = _realService.GetDocumentMetadata(documentId);
               _metadataCache[documentId] = metadata;

               return metadata;
          }
     }
}