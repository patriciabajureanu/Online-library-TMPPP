using System.Linq;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.ChainOfResponsibility
{
     public class BorrowLimitHandler : BaseHandler
     {
          private readonly OnlineLibraryDbContext _db;

          public BorrowLimitHandler(OnlineLibraryDbContext db)
          {
               _db = db;
          }

          public override AccessResult Handle(AccessRequest request)
          {
               int activeLoans = _db.Loans
                    .Count(l => l.UserEmail == request.UserId && l.ReturnDate == null);
               if (activeLoans >= 3)
               {
                    return new AccessResult(false, "Borrow limit reached. Maximum 3 active loans allowed.");
               }

               return base.Handle(request);
          }
     }
}