using System.Collections.Generic;

namespace OnlineLibrary.Proxy
{
     public class DocumentCacheProxy : IDocumentAccessService
     {
          private readonly IDocumentAccessService realService;
          private readonly Dictionary<string, string> cache;

          public DocumentCacheProxy(IDocumentAccessService realService)
          {
               this.realService = realService;
               this.cache = new Dictionary<string, string>();
          }

          public string GetDocument(string documentId)
          {
               string key = "doc_" + documentId;

               if (!cache.ContainsKey(key))
               {
                    cache[key] = realService.GetDocument(documentId);
                    return "[Loaded from service] " + cache[key];
               }

               return "[Loaded from cache] " + cache[key];
          }

          public string GetDocumentMetadata(string documentId)
          {
               string key = "meta_" + documentId;

               if (!cache.ContainsKey(key))
               {
                    cache[key] = realService.GetDocumentMetadata(documentId);
                    return "[Loaded metadata from service] " + cache[key];
               }

               return "[Loaded metadata from cache] " + cache[key];
          }
     }
}