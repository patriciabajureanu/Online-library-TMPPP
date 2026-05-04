using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.ChainOfResponsibility
{
     public class AvailabilityHandler : BaseHandler
     {
          private readonly OnlineLibraryDbContext _db;

          public AvailabilityHandler(OnlineLibraryDbContext db)
          {
               _db = db;
          }

          public override AccessResult Handle(AccessRequest request)
          {
               var book = _db.Books.FirstOrDefault(b => b.Id == request.BookId);

               if (book == null)
                    return new AccessResult(false, "Book not found.");

               if (book.AvailableCopies <= 0)
                    return new AccessResult(false, "This book is not available.");

               return base.Handle(request);
          }
     }
}