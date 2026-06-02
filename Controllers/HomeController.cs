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
using OnlineLibrary.ChainOfResponsibility;
using OnlineLibrary.Command;
using OnlineLibrary.Composite;
using OnlineLibrary.Data;
using OnlineLibrary.Decorator;
using OnlineLibrary.Facade;
using OnlineLibrary.FactoryMethod;
using OnlineLibrary.Flyweight;
using OnlineLibrary.Iterator;
using OnlineLibrary.Mediator;
using OnlineLibrary.Memento;
using OnlineLibrary.Models;
using OnlineLibrary.Observer;
using OnlineLibrary.Prototype;
using OnlineLibrary.Proxy;
using OnlineLibrary.Repositories;
using OnlineLibrary.State;
using OnlineLibrary.Strategy;
using OnlineLibrary.TemplateMethod;
using OnlineLibrary.Visitor;
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

               var newestBooks = books
                    .OrderByDescending(b => b.PublishedYear)
                    .ToList();
               var db = new OnlineLibraryDbContext();
               var bestBookId = db.Loans
                    .GroupBy(l => l.BookId)
                    .Select(g => new
                    {
                         BookId = g.Key,
                         BorrowCount = g.Count()
                    })
                    .OrderByDescending(x => x.BorrowCount)
                    .Select(x => x.BookId)
                    .FirstOrDefault();

               var bestBookDb = db.Books
     .Where(b => b.Id == bestBookId)
     .Select(b => new
     {
          b.Id,
          b.Title,
          b.Description,
          ImagePath = b.CoverImageUrl,
          b.FormatType,
          b.Language,
          b.PublishedYear,
          b.AvailableCopies,
          AuthorName = db.Authors
               .Where(a => a.Id == b.AuthorId)
               .Select(a => a.FullName)
               .FirstOrDefault()
     })
     .FirstOrDefault();
               var model = new HomeViewModel
               {
                    FeaturedBooks = newestBooks.Select(b => new BookViewModel
                    {
                         Id = b.Id,
                         Title = b.Title,
                         Description = b.Description,
                         ImagePath = b.ImagePath,
                         FormatType = b.FormatType,
                         Language = b.Language,
                         Publisher = b.Publisher,
                         PublishedYear = b.PublishedYear,
                         BookType = b.BookType,
                         CategoryName = b.CategoryName
                    }).ToList(),

                    TotalBooks = books.Count,

                    TotalSharedFormats = service.GetSharedFormatsCount(),

                    BestBook = bestBookDb != null ? new BookViewModel
                    {
                         Id = bestBookDb.Id,
                         Title = bestBookDb.Title,
                         Description = bestBookDb.Description,
                         ImagePath = bestBookDb.ImagePath,

                         FormatType = bestBookDb.FormatType,
                         Language = bestBookDb.Language,
                         PublishedYear = bestBookDb.PublishedYear,

                         AvailableCopies = bestBookDb.AvailableCopies,
                         AuthorName = bestBookDb.AuthorName ?? "Unknown Author"
                    } : null
               };

               return View(model);
          }
          [Authorize]
          public ActionResult Borrow()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books
                         .Include("Author")
                         .ToList();
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
          [ValidateAntiForgeryToken]
          public ActionResult BorrowBookConfirm(int id)
          {
               string userEmail = User.Identity.Name;

               using (var db = new OnlineLibraryDbContext())
               {
                    var activeLoansCount = db.Loans
                         .Count(l => l.UserEmail == userEmail && !l.IsReturned);

                    if (activeLoansCount >= 3)
                    {
                         TempData["Error"] = "You cannot borrow more than 3 active books.";
                         return RedirectToAction("BorrowBook", new { id = id });
                    }

                    var book = db.Books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                         TempData["Error"] = "Book not found.";
                         return RedirectToAction("Borrow");
                    }

                    if (book.AvailableCopies <= 0)
                    {
                         TempData["Error"] = "This book is not available.";
                         return RedirectToAction("BorrowBook", new { id = id });
                    }
               }

               var facade = new LibraryFacade();
               var result = facade.BorrowBook(userEmail, id);

               TempData["Success"] = "Book borrowed successfully!";

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

               return RedirectToAction("MyReservations");
          }
          [Authorize]
          public ActionResult MyReservations()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var userEmail = User.Identity.Name;

                    var reservations = db.Reservations
                         .Include("Book")
                         .Include("Book.Author")
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
               try
               {
                    var userEmail = User.Identity.Name;

                    var manager = new LibraryManager();
                    var command = new ReserveBookCommand(manager, id, userEmail);

                    command.Undo();

                    TempData["Success"] = "Reservation cancelled successfully.";
               }
               catch
               {
                    TempData["Error"] = "Reservation could not be cancelled.";
               }

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
          public ActionResult Composite()
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
                         TempData["Error"] = "...";
                         return RedirectToAction("MyLoans");
                    }

                    // PROXY PATTERN - Access control + cache
                    IDocumentAccessService proxyService = new RareDocumentService();

                    proxyService = new AccessControlProxy(proxyService, User.Identity.Name);
                    proxyService = new DocumentCacheProxy(proxyService);

                    var proxyResult = proxyService.GetDocument(id.ToString());

                    if (proxyResult.Contains("Access denied"))
                    {
                         TempData["Error"] = proxyResult;
                         return RedirectToAction("Index");
                    }

                    string virtualPath = book.FilePath;

                    if (string.IsNullOrEmpty(virtualPath))
                    {
                         TempData["Error"] = "...";
                         return RedirectToAction("MyLoans");
                    }

                    string physicalPath = Server.MapPath(virtualPath);

                    if (!System.IO.File.Exists(physicalPath))
                    {
                         TempData["Error"] = "...";
                         return RedirectToAction("MyLoans");
                    }

                    switch (deliveryType)
                    {
                         case "download":
                              TempData["BridgeMessage"] = proxyResult;
                              return File(physicalPath, "application/pdf", book.Title + ".pdf");

                         case "streaming":
                              TempData["BridgeMessage"] = proxyResult;
                              return RedirectToAction("Read", new { id = book.Id });

                         case "cloud":
                              TempData["BridgeMessage"] = proxyResult;
                              return Redirect(Url.Content(virtualPath));

                         default:
                              TempData["Error"] = "...";
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
                        .Include("Book.Author")
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

          public ActionResult ChainOfResponsibility(int bookId)
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    string userEmail = User.Identity.IsAuthenticated
                        ? User.Identity.Name
                        : null;

                    var request = new AccessRequest(userEmail, bookId);

                    var membership = new MembershipHandler();
                    var availability = new AvailabilityHandler(db);
                    var borrowLimit = new BorrowLimitHandler(db);

                    membership
                        .SetNext(availability)
                        .SetNext(borrowLimit);

                    var result = membership.Handle(request);

                    ViewBag.Result = result;
                    ViewBag.Book = db.Books.FirstOrDefault(b => b.Id == bookId);

                    return View();
               }
          }
          [Authorize]
          public ActionResult State()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var users = db.Users.ToList();

                    var result = users.Select(user =>
                    {
                         var activeLoans = db.Loans
                              .Count(l =>
                                   l.UserEmail == user.Email &&
                                   !l.IsReturned);

                         string membershipState;

                         if (activeLoans == 0)
                         {
                              membershipState = "Inactive";
                         }
                         else if (activeLoans < 3)
                         {
                              membershipState = "Active";
                         }
                         else
                         {
                              membershipState = "Limit Reached";
                         }

                         return new MembershipStateViewModel
                         {
                              Username = user.Username,
                              Email = user.Email,
                              Role = user.Role,
                              ActiveLoans = activeLoans,
                              MembershipState = membershipState
                         };
                    }).ToList();

                    return View(result);
               }
          }
          public ActionResult Mediator(string searchText = "", string category = "")
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books
                    .Include("Category")
                    .ToList();

                    var mediator = new LibraryMediator(books);

                    var search = new SearchComponent(mediator);
                    var filter = new FilterComponent(mediator);
                    var results = new ResultsComponent();

                    mediator.Search = search;
                    mediator.Filter = filter;
                    mediator.Results = results;

                    if (!string.IsNullOrEmpty(searchText))
                    {
                         search.Search(searchText);
                    }
                    else if (!string.IsNullOrEmpty(category))
                    {
                         filter.SelectFilter(category);
                    }
                    else
                    {
                         results.UpdateResults(books);
                    }

                    ViewBag.Results = results.CurrentResults;
                    ViewBag.Categories = db.Categories.Select(c => c.Name).ToList();

                    return View();
               }
          }
          public ActionResult TemplateMethod(string type = null)
          {
               if (string.IsNullOrEmpty(type))
               {
                    return View();
               }

               using (var db = new OnlineLibraryDbContext())
               {
                    string reportId = DateTime.Now.ToString("yyyyMMddHHmmss");

                    LibraryReportGenerator generator;

                    if (type == "csv")
                         generator = new CsvLibraryReportGenerator(db);
                    else
                         generator = new PdfLibraryReportGenerator(db);

                    string filePath = generator.GenerateReport(reportId);

                    ViewBag.Type = type.ToUpper();
                    ViewBag.FilePath = filePath;

                    return View();
               }
          }
          public ActionResult Visitor()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books
                        .Include("Category")
                        .ToList();

                    var resources = new List<ILibraryResource>();

                    foreach (var book in books)
                    {
                         var format = book.FormatType != null ? book.FormatType.ToLower() : "";

                         if (format.Contains("audio"))
                         {
                              resources.Add(new Audiobook(book.Title, book.Pages > 0 ? book.Pages * 2 : 120));
                         }
                         else if (format.Contains("magazine"))
                         {
                              resources.Add(new Magazine(book.Title, book.Id));
                         }
                         else
                         {
                              resources.Add(new Ebook(book.Title, book.Pages > 0 ? book.Pages / 10 : 10));
                         }
                    }

                    var sizeVisitor = new SizeCalculatorVisitor();
                    var previewVisitor = new PreviewVisitor();

                    foreach (var resource in resources)
                    {
                         resource.Accept(sizeVisitor);
                         resource.Accept(previewVisitor);
                    }

                    ViewBag.SizeResults = sizeVisitor.Results;
                    ViewBag.PreviewResults = previewVisitor.Results;

                    return View();
               }
          }
     }
     }