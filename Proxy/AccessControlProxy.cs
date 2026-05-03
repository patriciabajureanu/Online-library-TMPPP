using OnlineLibrary.Data;
using System.Linq;

namespace OnlineLibrary.Proxy
{
     public class AccessControlProxy : IDocumentAccessService
     {
          private readonly IDocumentAccessService _realService;
          private readonly string _userEmail;

          public AccessControlProxy(IDocumentAccessService realService, string userEmail)
          {
               _realService = realService;
               _userEmail = userEmail;
          }

          public string GetDocument(string documentId)
          {
               int bookId = int.Parse(documentId);

               using (var db = new OnlineLibraryDbContext())
               {
                    bool hasLoan = db.Loans.Any(l =>
                        l.BookId == bookId &&
                        l.UserEmail == _userEmail &&
                        !l.IsReturned);

                    if (!hasLoan)
                         return "Access denied. You must borrow this document first.";
               }

               return _realService.GetDocument(documentId);
          }

          public string GetDocumentMetadata(string documentId)
          {
               return _realService.GetDocumentMetadata(documentId);
          }
     }
}