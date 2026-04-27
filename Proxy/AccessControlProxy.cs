using OnlineLibrary.Proxy;

namespace OnlineLibrary.Patterns.Proxy
{
     public class AccessControlProxy : IDocumentAccessService
     {
          private readonly IDocumentAccessService _realService;
          private readonly bool _hasMembership;

          public AccessControlProxy(IDocumentAccessService realService, bool hasMembership)
          {
               _realService = realService;
               _hasMembership = hasMembership;
          }

          public string GetDocument(string documentId)
          {
               if (!_hasMembership)
               {
                    return "Access denied. The user does not have membership.";
               }

               return _realService.GetDocument(documentId);
          }

          public string GetDocumentMetadata(string documentId)
          {
               if (!_hasMembership)
               {
                    return "Access denied to metadata. The user does not have membership.";
               }

               return _realService.GetDocumentMetadata(documentId);
          }
     }
}