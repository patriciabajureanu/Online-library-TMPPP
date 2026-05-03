using OnlineLibrary.Data;
using System.Linq;

namespace OnlineLibrary.Proxy
{
     public class RareDocumentService : IDocumentAccessService
     {
          public string GetDocument(string documentId)
          {
               int bookId = int.Parse(documentId);

               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == bookId);

                    if (book == null)
                         return "Document not found.";

                    return $"Rare document loaded: {book.Title}";
               }
          }

          public string GetDocumentMetadata(string documentId)
          {
               int bookId = int.Parse(documentId);

               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == bookId);

                    if (book == null)
                         return "Metadata not found.";

                    return $"Title: {book.Title}, Type: {book.BookType}, Language: {book.Language}";
               }
          }
     }
}