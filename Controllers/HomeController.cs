using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Bridge;
using OnlineLibrary.Decorator;
using OnlineLibrary.Flyweight;
using OnlineLibrary.Models;
using OnlineLibrary.Patterns.Bridge;
using OnlineLibrary.Patterns.Proxy;
using OnlineLibrary.Proxy;
using OnlineLibrary.Strategy;
using OnlineLibrary.Observer;
using OnlineLibrary.Command;
using OnlineLibrary.Memento;
using OnlineLibrary.Iterator;
using OnlineLibrary.Data;

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

               var model = new HomeViewModel
               {
                    FeaturedBooks = sortedBooks.Select(b => new BookViewModel
                    {
                         Id = b.Id,
                         Title = b.Title,
                         FormatType = b.Format.FormatType,
                         Language = b.Format.Language,
                         Publisher = b.Format.Publisher
                    }).ToList(),

                    TotalBooks = books.Count,

                    TotalSharedFormats = books
                       .Select(b => $"{b.Format.FormatType}-{b.Format.Language}-{b.Format.Publisher}")
                       .Distinct()
                       .Count()
               };

               return View(model);
          }
          public ActionResult Read(string id)
          {
               IBookAccessService service = new BasicBookAccessService();
               service = new LoggingDecorator(service);
               service = new CachingDecorator(service);
               service = new AuthorizationDecorator(service);

               var content = service.GetBookContent(id);

               ViewBag.BookId = id;
               ViewBag.Content = content;

               return View();
          }
          public ActionResult BridgeDemo()
          {
               var items = new List<BridgeDemoItemViewModel>();

               LibraryResource ebookDownload = new EbookResource(new DownloadDelivery());
               LibraryResource ebookStreaming = new EbookResource(new StreamingDelivery());
               LibraryResource audiobookStreaming = new AudiobookResource(new StreamingDelivery());
               LibraryResource magazineCloud = new MagazineResource(new CloudDelivery());

               items.Add(new BridgeDemoItemViewModel
               {
                    ResourceType = "Ebook",
                    DeliveryType = "Download",
                    ResourceId = "E001",
                    Result = ebookDownload.Access("E001")
               });

               items.Add(new BridgeDemoItemViewModel
               {
                    ResourceType = "Ebook",
                    DeliveryType = "Streaming",
                    ResourceId = "E002",
                    Result = ebookStreaming.Access("E002")
               });

               items.Add(new BridgeDemoItemViewModel
               {
                    ResourceType = "Audiobook",
                    DeliveryType = "Streaming",
                    ResourceId = "A001",
                    Result = audiobookStreaming.Access("A001")
               });

               items.Add(new BridgeDemoItemViewModel
               {
                    ResourceType = "Magazine",
                    DeliveryType = "Cloud",
                    ResourceId = "M001",
                    Result = magazineCloud.Access("M001")
               });

               var model = new BridgeDemoViewModel
               {
                    Items = items
               };

               return View(model);
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

               collection.AddLoan(new Loan { Id = "1", UserId = "U1", BookTitle = "Clean Code", BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) });
               collection.AddLoan(new Loan { Id = "2", UserId = "U1", BookTitle = "Design Patterns", BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(5) });
               collection.AddLoan(new Loan { Id = "3", UserId = "U2", BookTitle = "Refactoring", BorrowDate = DateTime.Now, DueDate = DateTime.Now.AddDays(10) });

               var iterator = collection.CreateIterator();

               var result = new List<Loan>();

               while (iterator.HasMore())
               {
                    result.Add(iterator.GetNext());
               }

               return View(result);
          }

          [AllowAnonymous]
          public ActionResult DbBooks()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var books = db.Books.ToList();

                    ViewBag.Count = books.Count; // debug rapid

                    return View(books);
               }
          }
     }
     }