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
using OnlineLibrary.Builder;
using OnlineLibrary.Command;
using OnlineLibrary.Composite;
using OnlineLibrary.Data;
using OnlineLibrary.Decorator;
using OnlineLibrary.Facade;
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
          [HttpGet]
          public ActionResult BorrowBook(int id)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                         return HttpNotFound();

                    return View(book);
               }
          }
          [Authorize]
          [HttpPost]
          public ActionResult BorrowBookConfirm(int id)
          {
               string userId = User.Identity.Name;

               var facade = new LibraryFacade();
               var result = facade.BorrowBook(userId, id);

               TempData["Success"] = "📚 Book borrowed successfully!";

               return RedirectToAction("MyLoans");
          }
          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult ReserveBook(int id)
          {
               var userEmail = User.Identity.Name;

               var manager = new LibraryManager();
               var command = new ReserveBookCommand(manager, id, userEmail);

               var invoker = new LibraryInvoker();
               invoker.ExecuteCommand(command);

               TempData["Success"] = "📌 Reservation successful!";

               return RedirectToAction("MyLoans");
          }
          [Authorize]
          public ActionResult MyReservations()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var userEmail = User.Identity.Name;

                    var reservations = db.Reservations
                         .Include("Book")
                         .Where(r => r.UserEmail == userEmail)
                         .OrderByDescending(r => r.ReservedAt)
                         .ToList();

                    return View(reservations);
               }
          }
          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult CancelReservation(int id)
          {
               var userEmail = User.Identity.Name;

               var manager = new LibraryManager();
               var command = new ReserveBookCommand(manager, id, userEmail);

               command.Undo();

               TempData["Success"] = "Reservation cancelled successfully.";

               return RedirectToAction("MyReservations");
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
          [Authorize]
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
                    var facade = new LibraryFacade();
                    ViewBag.DecoratorResult = facade.GetBookContent(User.Identity.Name, id);

                    var externalPdfReader = new ExternalPdfReader();
                    var adapter = new PdfReaderAdapter(externalPdfReader);
                    var libraryReaderService = new LibraryReaderService(adapter);

                    ViewBag.BookTitle = book.Title;
                    ViewBag.FilePath = book.FilePath;
                    ViewBag.BookId = book.Id;
                    ViewBag.TotalPages = book.Pages > 0 ? book.Pages : 100;

                    ViewBag.OpenResult = libraryReaderService.ReadBook(book.FilePath);
                    ViewBag.PageResult = libraryReaderService.NavigateToPage(page);
                    ViewBag.CloseResult = libraryReaderService.CloseBook();
                    var userEmail = User.Identity.Name;

                    var sessionKey = Session.SessionID;

                    var progress = db.ReadingProgresses
                        .FirstOrDefault(p => p.BookId == id
                                          && p.UserEmail == userEmail
                                          && p.SessionKey == sessionKey);

                    if (progress != null)
                    {
                         ViewBag.CurrentPage = progress.CurrentPage;
                         ViewBag.Theme = progress.Theme;
                         ViewBag.FontSize = progress.FontSize;

                         ViewBag.ProgressPercent = ViewBag.TotalPages > 0
                              ? (progress.CurrentPage * 100 / ViewBag.TotalPages)
                              : 0;
                    }
                    else
                    {
                         ViewBag.CurrentPage = page;
                         ViewBag.Theme = "Light";
                         ViewBag.FontSize = "Medium";

                         ViewBag.ProgressPercent = ViewBag.TotalPages > 0
                              ? (page * 100 / ViewBag.TotalPages)
                              : 0;
                    }
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

          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult SaveReadingProgress(int bookId, int currentPage, string theme, string fontSize)
          {
               var userEmail = User.Identity.Name;

               var session = new ReadingSession();
               var history = new ReadingHistory();

               session.SetState(currentPage, theme, fontSize);
               history.MakeBackup(session);

               using (var db = new OnlineLibraryDbContext())
               {
                    var progress = db.ReadingProgresses
                        .FirstOrDefault(p => p.BookId == bookId && p.UserEmail == userEmail);

                    if (progress == null)
                    {
                         progress = new ReadingProgress
                         {
                              UserEmail = userEmail,
                              BookId = bookId,
                              CurrentPage = session.CurrentPage,
                              Theme = session.Theme,
                              FontSize = session.FontSize,
                              SavedAt = DateTime.Now
                         };

                         db.ReadingProgresses.Add(progress);
                    }
                    else
                    {
                         progress.CurrentPage = session.CurrentPage;
                         progress.Theme = session.Theme;
                         progress.FontSize = session.FontSize;
                         progress.SavedAt = DateTime.Now;
                    }

                    db.SaveChanges();
               }

               TempData["Success"] = "Reading progress saved successfully!";
               return RedirectToAction("Read", new { id = bookId, page = currentPage });
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

          [HttpPost]
          [Authorize]
          public JsonResult AutoSaveReadingProgress(int bookId, int currentPage, string theme, string fontSize)
          {
               var userEmail = User.Identity.Name;
               var sessionKey = Session.SessionID;

               using (var db = new OnlineLibraryDbContext())
               {
                    var progress = db.ReadingProgresses
                        .FirstOrDefault(p => p.BookId == bookId
                                          && p.UserEmail == userEmail
                                          && p.SessionKey == sessionKey);

                    if (progress == null)
                    {
                         progress = new ReadingProgress
                         {
                              UserEmail = userEmail,
                              BookId = bookId,
                              CurrentPage = currentPage,
                              PreviousPage = currentPage,
                              Theme = theme,
                              FontSize = fontSize,
                              SessionKey = sessionKey,
                              SavedAt = DateTime.Now
                         };

                         db.ReadingProgresses.Add(progress);
                    }
                    else
                    {
                         progress.PreviousPage = progress.CurrentPage;
                         progress.CurrentPage = currentPage;
                         progress.Theme = theme;
                         progress.FontSize = fontSize;
                         progress.SavedAt = DateTime.Now;
                    }

                    db.SaveChanges();
               }

               return Json(new { success = true });
          }
          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult UndoReadingProgress(int bookId)
          {
               var userEmail = User.Identity.Name;
               var sessionKey = Session.SessionID;

               using (var db = new OnlineLibraryDbContext())
               {
                    var progress = db.ReadingProgresses
                        .FirstOrDefault(p => p.BookId == bookId
                                          && p.UserEmail == userEmail
                                          && p.SessionKey == sessionKey);

                    if (progress != null)
                    {
                         progress.CurrentPage = progress.PreviousPage;
                         progress.SavedAt = DateTime.Now;
                         db.SaveChanges();

                         TempData["Success"] = "Reading progress restored.";
                         return RedirectToAction("Read", new { id = bookId, page = progress.CurrentPage });
                    }
               }

               TempData["Error"] = "No previous reading progress found.";
               return RedirectToAction("Read", new { id = bookId });
          }



          [Authorize]
          public ActionResult MyLoans()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var userEmail = User.Identity.Name;

                    var loansFromDb = db.Loans
                        .Include("Book")
                        .Where(l => l.UserEmail == userEmail)
                        .OrderByDescending(l => l.BorrowDate)
                        .ToList();

                    var collection = new UserLoanCollection(loansFromDb);
                    var iterator = collection.CreateIterator();

                    var loans = new List<Loan>();

                    while (iterator.HasMore())
                    {
                         var loan = iterator.GetNext();

                         if (!loan.IsReturned && loan.DueDate.HasValue && loan.DueDate.Value < DateTime.Now)
                         {

                              ViewBag.HasOverdueLoans = true;
                         }

                         loans.Add(loan);
                    }

                    return View(loans);
               }
          }
          [HttpPost]
          [Authorize]
          [ValidateAntiForgeryToken]
          public ActionResult ReturnBook(int id)
          {
               var userEmail = User.Identity.Name;

               var manager = new LibraryManager();
               //Command
               var command = new ReturnBookCommand(manager, id, userEmail);

               var invoker = new LibraryInvoker();
               invoker.ExecuteCommand(command);

               TempData["Success"] = "Book returned successfully!";
               TempData["ObserverMessage"] = "Observer: email, SMS and log were notified for returned book.";

               return RedirectToAction("MyLoans");
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