using OnlineLibrary.Facade.Subsystems;

namespace OnlineLibrary.Facade
{
     public class LibraryFacade
     {
          private readonly UserValidator _validator;
          private readonly UserRepository _repository;
          private readonly BookCatalog _catalog;
          private readonly LoanService _loanService;
          private readonly NotificationService _notification;
          private readonly AuditLogger _audit;

          public LibraryFacade(
              UserValidator validator,
              UserRepository repository,
              BookCatalog catalog,
              LoanService loanService,
              NotificationService notification,
              AuditLogger audit)
          {
               _validator = validator;
               _repository = repository;
               _catalog = catalog;
               _loanService = loanService;
               _notification = notification;
               _audit = audit;
          }

          public string BorrowBook(string userId, string bookId)
          {
               if (!_validator.Validate(userId))
                    return "User invalid";

               if (!_catalog.CheckAvailability(bookId))
                    return "Book not available";

               string loanId = _loanService.CreateLoan(userId, bookId);

               _notification.SendConfirmation(userId, $"Book {bookId} borrowed successfully! Loan ID: {loanId}");
               _audit.LogAction(userId, $"Borrowed book {bookId} with loan {loanId}");

               return $"Success! Loan ID: {loanId}";
          }
     }
}