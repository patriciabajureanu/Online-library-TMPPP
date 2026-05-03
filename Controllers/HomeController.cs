using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.AbstractFactory;
using OnlineLibrary.Adapter.Adaptee;
using OnlineLibrary.Adapter.Adapters;
using OnlineLibrary.Adapter.Services;
using OnlineLibrary.Bridge;
using OnlineLibrary.Bridge;
using OnlineLibrary.Builder;
using OnlineLibrary.Command;
using OnlineLibrary.Composite;
using OnlineLibrary.Data;
using OnlineLibrary.Decorator;
using OnlineLibrary.FactoryMethod;
using OnlineLibrary.Flyweight;
using OnlineLibrary.Iterator;
using OnlineLibrary.Memento;
using OnlineLibrary.Models;
using OnlineLibrary.Observer;
using OnlineLibrary.Patterns.Proxy;
using OnlineLibrary.Prototype;
using OnlineLibrary.Proxy;
using OnlineLibrary.Repositories;
using OnlineLibrary.Strategy;
using Rotativa;

namespace OnlineLibrary.Controllers
{
     [Authorize]
     public class HomeController : Controller
     {
          public ActionResult Index(string sortType)
          {
               var service = new LibraryService();
               var books = service.LoadBooks();
               var context = new BookCatalogContext();

               switch (sortType)
               {
                    case "id":
                         context.SetStrategy(new SortByIdStrategy());
                         break;

                    case "format":
                         context.SetStrategy(new SortByFormatStrategy());
                         break;

                    case "title":
                    default:
                         context.SetStrategy(new SortByTitleStrategy());
                         break;
               }

               var sortedBooks = context.SortBooks(books);
               var bestBook = sortedBooks.FirstOrDefault();
               var model = new HomeViewModel
               {
                    FeaturedBooks = sortedBooks.Select(b => new BookViewModel
                    {
                         Id = b.Id,
                         Title = b.Title,
                         Description = b.Description,
                         ImagePath = b.ImagePath,

                         FormatType = b.FormatType,
                         Language = b.Language,
                         Publisher = b.Publisher,

                         PublishedYear = b.PublishedYear,
                         CategoryName = b.CategoryName
                    }).ToList(),

                    TotalBooks = books.Count,

                    TotalSharedFormats = service.GetSharedFormatsCount(),

                    BestBook = bestBook != null ? new BookViewModel
                    {
                         Id = bestBook.Id,
                         Title = bestBook.Title,
                         Description = bestBook.Description,
                         ImagePath = bestBook.ImagePath,

                         FormatType = bestBook.FormatType,
                         Language = bestBook.Language,
                         Publisher = bestBook.Publisher,

                         PublishedYear = bestBook.PublishedYear,
                         CategoryName = bestBook.CategoryName
                    } : null
               };

               return View(model);
          }
          [Authorize]
          public ActionResult Borrow()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books.ToList();

                    ViewBag.TotalBooks = books.Count;

                    ViewBag.TotalSharedFormats = books
                         .Select(b => "PDF-" + (b.Language ?? "Unknown") + "-" +
                              (b.PublisherId.HasValue ? b.PublisherId.Value.ToString() : "Unknown"))
                         .Distinct()
                         .Count();

                    return View(books);
               }
          }
          [Authorize]
          public ActionResult BorrowBook(int id)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                         return HttpNotFound();
                    }

                    return View(book);
               }
          }

          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult ConfirmBorrowBook(int id, string userType)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                         return HttpNotFound();
                    }

                    if (book.AvailableCopies <= 0)
                    {
                         TempData["Error"] = "This book is not available.";
                         return RedirectToAction("BorrowBook", new { id = id });
                    }

                    // ABSTRACT FACTORY
                    IUserRepository userRepository = new UserRepository();

                    var currentEmail = User.Identity.Name;
                    var loggedUser = userRepository.GetByEmail(currentEmail);

                    if (loggedUser == null)
                    {
                         TempData["Error"] = "User not found. Please login again.";
                         return RedirectToAction("Login", "Account");
                    }

                    var role = loggedUser.Role;

                    Session["Role"] = role; // îl refacem și în sesiune

                    var factory = UserFactoryProvider.GetFactory(role);


                    var user = factory.CreateUser(User.Identity.Name);
                    var loanType = factory.CreateLoan(book.Title);

                    var loan = new Loan
                    {
                         BookId = book.Id,
                         UserEmail = user.GetName(),
                         UserType = user.GetUserType(),
                         BorrowDate = DateTime.Now,
                         DueDate = DateTime.Now.AddDays(loanType.GetLoanDays()),
                         ReturnDate = null,
                         IsReturned = false
                    };

                    db.Loans.Add(loan);

                    book.AvailableCopies--;

                    db.SaveChanges();

                    TempData["Success"] = "Book borrowed successfully!";
                    return RedirectToAction("MyLoans");
               }
          }

          public ActionResult DownloadPrototypeReport()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books.ToList();

                    var masterReport = new AnalyticalReport
                    {
                         Title = "Online Library Inventory Report",
                         HeaderColor = "Gold",
                         ChartType = "Table",
                         IncludeDataRaw = true
                    };

                    masterReport.Sections.Add("Generated at: " + DateTime.Now);
                    masterReport.Sections.Add("Total books: " + books.Count);
                    masterReport.Sections.Add("Available copies: " + books.Sum(b => b.AvailableCopies));
                    masterReport.Sections.Add("Borrowed copies: " + books.Sum(b => b.TotalCopies - b.AvailableCopies));

                    var clonedReport = (AnalyticalReport)masterReport.Clone();
                    clonedReport.Title = "Downloaded Prototype Report";

                    ViewBag.Report = clonedReport;
                    ViewBag.Books = books;

                    return new ViewAsPdf("PrototypeReportPdf")
                    {
                         FileName = "OnlineLibrary_Prototype_Report.pdf",
                         PageSize = Rotativa.Options.Size.A4,
                         PageOrientation = Rotativa.Options.Orientation.Portrait
                    };
               }
          }

          public ActionResult Read(int id, int page = 1)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                         return HttpNotFound();
                    }

                    if (string.IsNullOrEmpty(book.FilePath))
                    {
                         TempData["Error"] = "This book does not have a PDF file attached.";
                         return RedirectToAction("Index");
                    }

                    var externalPdfReader = new ExternalPdfReader();
                    var adapter = new PdfReaderAdapter(externalPdfReader);
                    var libraryReaderService = new LibraryReaderService(adapter);

                    ViewBag.BookTitle = book.Title;
                    ViewBag.FilePath = book.FilePath;

                    ViewBag.OpenResult = libraryReaderService.ReadBook(book.FilePath);
                    ViewBag.PageResult = libraryReaderService.NavigateToPage(page);
                    ViewBag.CloseResult = libraryReaderService.CloseBook();

                    return View();
               }
          }
          public ActionResult CompositeDemo()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var categories = db.Categories.ToList();
                    var authors = db.Authors.ToList();
                    var books = db.Books.ToList();

                    var categoryComponents = new List<CategoryComponent>();

                    foreach (var category in categories)
                    {
                         var categoryComponent = new CategoryComponent(category.Name);

                         var booksInCategory = books
                              .Where(b => b.CategoryId == category.Id)
                              .ToList();

                         var authorIds = booksInCategory
                              .Where(b => b.AuthorId != null)
                              .Select(b => b.AuthorId.Value)
                              .Distinct()
                              .ToList();

                         foreach (var authorId in authorIds)
                         {
                              var author = authors.FirstOrDefault(a => a.Id == authorId);

                              if (author == null)
                                   continue;

                              var authorComponent = new AuthorComponent(author.FullName);

                              var authorBooks = booksInCategory
                                   .Where(b => b.AuthorId == author.Id)
                                   .ToList();

                              foreach (var book in authorBooks)
                              {
                                   var pages = book.Pages > 0 ? book.Pages : 100;

                                   authorComponent.Add(
                                        new BookComponent(book.Title, pages)
                                   );
                              }

                              categoryComponent.Add(authorComponent);
                         }

                         categoryComponents.Add(categoryComponent);
                    }

                    ViewBag.Categories = categoryComponents;

                    return View();
               }
          }
          [Authorize]
          public ActionResult AccessLoanResource(string resourceType, string deliveryType, int id)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                         TempData["BridgeMessage"] = "Book not found.";
                         return RedirectToAction("MyLoans");
                    }

                    string virtualPath = book.FilePath;

                    if (string.IsNullOrEmpty(virtualPath))
                    {
                         TempData["BridgeMessage"] = "This book does not have a PDF file.";
                         return RedirectToAction("MyLoans");
                    }

                    string physicalPath = Server.MapPath(virtualPath);

                    if (!System.IO.File.Exists(physicalPath))
                    {
                         TempData["BridgeMessage"] = "PDF file not found: " + virtualPath;
                         return RedirectToAction("MyLoans");
                    }

                    switch (deliveryType)
                    {
                         case "download":
                              return File(physicalPath, "application/pdf", book.Title + ".pdf");

                         case "streaming":
                              return RedirectToAction("Read", new { id = book.Id.ToString() });

                         case "cloud":
                              return Redirect(Url.Content(virtualPath));

                         default:
                              TempData["BridgeMessage"] = "Unknown delivery method.";
                              return RedirectToAction("MyLoans");
                    }
               }
          }
          public ActionResult ProxyDemo()
          {
               string documentId = "DOC-101";
               bool hasMembership = true;

               IDocumentAccessService service = new RareDocumentService();
               service = new AccessControlProxy(service, hasMembership);
               service = new DocumentCacheProxy(service);

               var model = new ProxyDemoViewModel
               {
                    DocumentId = documentId,
                    HasMembership = hasMembership,
                    MetadataResult = service.GetDocumentMetadata(documentId),
                    DocumentResult = service.GetDocument(documentId),
                    CachedDocumentResult = service.GetDocument(documentId)
               };

               return View(model);
          }
          public ActionResult ObserverDemo(string id = "1")
          {
               var service = new LibraryEventService();

               ViewBag.BorrowResult = service.BorrowBook(id);
               ViewBag.ReturnResult = service.ReturnBook(id);
               ViewBag.ReserveResult = service.ReserveBook(id);

               return View();
          }
          public ActionResult CommandDemo(string id = "1")
          {
               var manager = new LibraryManager();
               var invoker = new LibraryInvoker();

               var borrowCommand = new BorrowBookCommand(manager, id);
               invoker.ExecuteCommand(borrowCommand);

               var reserveCommand = new ReserveBookCommand(manager, id);
               invoker.ExecuteCommand(reserveCommand);

               invoker.UndoLastCommand();

               ViewBag.BorrowResult = borrowCommand.Result;
               ViewBag.ReserveResult = reserveCommand.Result;

               return View();

          }
          public ActionResult MementoDemo()
          {
               var session = new ReadingSession();
               var history = new ReadingHistory();

               session.SetState("Book: Clean Code | Page: 25 | Theme: Light");
               history.MakeBackup(session);

               string savedState = session.GetState();

               session.SetState("Book: Clean Code | Page: 80 | Theme: Dark");
               string changedState = session.GetState();

               history.Undo(session);
               string restoredState = session.GetState();

               ViewBag.SavedState = savedState;
               ViewBag.ChangedState = changedState;
               ViewBag.RestoredState = restoredState;

               return View();
          }

          public ActionResult IteratorDemo()
          {
               var collection = new UserLoanCollection();

               collection.AddLoan(new Loan
               {
                    Id = 1,
                    BookId = 1,
                    UserEmail = "user1@test.com",
                    BorrowDate = DateTime.Now,
                    ReturnDate = null,
                    IsReturned = false
               });

               collection.AddLoan(new Loan
               {
                    Id = 2,
                    BookId = 2,
                    UserEmail = "user1@test.com",
                    BorrowDate = DateTime.Now,
                    ReturnDate = null,
                    IsReturned = false
               });

               collection.AddLoan(new Loan
               {
                    Id = 3,
                    BookId = 3,
                    UserEmail = "user2@test.com",
                    BorrowDate = DateTime.Now,
                    ReturnDate = null,
                    IsReturned = false
               });

               var iterator = collection.CreateIterator();

               var result = new List<Loan>();

               while (iterator.HasMore())
               {
                    result.Add(iterator.GetNext());
               }

               return View(result);
          }
          [Authorize]
          public ActionResult MyLoans()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var userEmail = User.Identity.Name;

                    var loans = db.Loans
                        .Include("Book")
                        .Where(l => l.UserEmail == userEmail)
                        .OrderByDescending(l => l.BorrowDate)
                        .ToList();

                    return View(loans);
               }
          }
          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult ReturnBook(int id)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var userEmail = User.Identity.Name;

                    var loan = db.Loans
                        .Include("Book")
                        .FirstOrDefault(l => l.Id == id && l.UserEmail == userEmail && !l.IsReturned);

                    if (loan == null)
                    {
                         TempData["Error"] = "Loan not found or already returned.";
                         return RedirectToAction("MyLoans");
                    }

                    loan.IsReturned = true;
                    loan.ReturnDate = DateTime.Now;

                    if (loan.Book != null)
                    {
                         loan.Book.AvailableCopies++;
                    }

                    db.SaveChanges();

                    TempData["Success"] = "Book returned successfully!";
                    return RedirectToAction("MyLoans");
               }
          }

          [AllowAnonymous]
          public ActionResult DbBooks()
          {
               IBookRepository bookRepository = new BookRepository();
               var books = bookRepository.GetAll();

               return View(books);
          }

          [AllowAnonymous]
          public ActionResult Details(int? id)
          {
               if (id == null)
                    return RedirectToAction("DbBooks");

               IBookRepository bookRepository = new BookRepository();
               var book = bookRepository.GetById(id.Value);

               if (book == null)
                    return HttpNotFound();

               return View(book);
          }
          
          
     }
     }