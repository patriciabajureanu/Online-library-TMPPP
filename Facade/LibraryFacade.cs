using System.Linq;
using Microsoft.Ajax.Utilities;
using OnlineLibrary.Command;
using OnlineLibrary.Data;
using OnlineLibrary.Decorator;
using OnlineLibrary.Facade;
using OnlineLibrary.Models;
using OnlineLibrary.Observer; // dacă folosești notificări

namespace OnlineLibrary.Facade
{
     public class LibraryFacade
     {
          private readonly UserRepository _userRepository;
          private readonly UserValidator _userValidator;
          private readonly BookCatalog _bookCatalog;
          private readonly LoanService _loanService;
          private readonly NotificationService _notificationService;
          private readonly AuditLogger _auditLogger;

          public LibraryFacade()
          {
               _userRepository = new UserRepository();
               _userValidator = new UserValidator();
               _bookCatalog = new BookCatalog();
               _loanService = new LoanService();
               _notificationService = new NotificationService();
               _auditLogger = new AuditLogger();
          }

          public string BorrowBook(string userId, int bookId)
          {
               // 1. Validare user
               if (!_userValidator.Validate(userId))
                    return "User invalid";

               // 2. Get user
               var user = _userRepository.GetUser(userId);

               if (user == null)
                    return "User not found";

               // 3. Verificare carte
               if (!_bookCatalog.CheckAvailability(bookId))
                    return "Book not available";

               // 4. Executare Command (CORECT)
               var manager = new LibraryManager();

               var command = new BorrowBookCommand(
                   manager,
                   bookId,
                   userId,
                   user.Role   // rol REAL
               );

               var invoker = new LibraryInvoker();
               invoker.ExecuteCommand(command);

               // 5. Notificare
               var book = _bookCatalog.GetBook(bookId);

               _notificationService.SendBorrowEmail(userId, book.Title);

               // 6. Logging
               _auditLogger.LogAction(userId, $"Borrowed book {bookId}");

               return "Borrow successful";
          }
          public string GetBookContent(string userEmail, int bookId)
          {
               bool hasAccess;

               using (var db = new OnlineLibraryDbContext())
               {
                    hasAccess = db.Loans.Any(l =>
                        l.BookId == bookId &&
                        l.UserEmail == userEmail &&
                        !l.IsReturned);
               }

               IBookAccessService service = new BasicBookAccessService();

               service = new CachingDecorator(service);
               service = new AuthorizationDecorator(service, hasAccess);
               service = new LoggingDecorator(service, userEmail);

               return service.GetBookContent(bookId.ToString());
          }
     }
}