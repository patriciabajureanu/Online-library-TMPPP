using System.Text;
using System.Web.Mvc;
using OnlineLibrary.Composite;
using OnlineLibrary.Facade;
using OnlineLibrary.Facade.Subsystems;

namespace OnlineLibrary.Controllers
{
     public class LibraryController : Controller
     {
          private readonly LibraryFacade _facade;

          public LibraryController()
          {
               // Construim Façade cu subsistemele
               _facade = new LibraryFacade(
                   new UserValidator(),
                   new UserRepository(),
                   new BookCatalog(),
                   new LoanService(),
                   new NotificationService(),
                   new AuditLogger()
               );
          }

          public ActionResult Index()
          {
               // Exemplu: userul "123" împrumută cartea "Book001"
               string result = _facade.BorrowBook("123", "Book001");
               ViewBag.Result = result;
               // Construim biblioteca
               var asimov = new Author("Isaac Asimov");
               asimov.Add(new Book("Foundation", 255));
               asimov.Add(new Book("I, Robot", 224));

               var herbert = new Author("Frank Herbert");
               herbert.Add(new Book("Dune", 412));
               herbert.Add(new Book("Neuromancer", 271));

               var sciFiGenre = new Genre("Science Fiction");
               sciFiGenre.Add(asimov);
               sciFiGenre.Add(herbert);

               var tolkien = new Author("J.R.R. Tolkien");
               tolkien.Add(new Book("The Hobbit", 310));
               tolkien.Add(new Book("Lord of the Rings", 1178));

               var rowling = new Author("J.K. Rowling");
               rowling.Add(new Book("Harry Potter and the Sorcerer's Stone", 223));

               var fantasyGenre = new Genre("Fantasy");
               fantasyGenre.Add(tolkien);
               fantasyGenre.Add(rowling);

               var library = new Genre("Online Library");
               library.Add(sciFiGenre);
               library.Add(fantasyGenre);

               // Pregătim afișarea pentru view
               var sb = new StringBuilder();
               BuildDisplay(library, sb);

               ViewBag.LibraryDisplay = sb.ToString();
               ViewBag.TotalBooks = library.GetTotalBooks();
               ViewBag.SciFiTotal = sciFiGenre.GetTotalBooks();
               ViewBag.FantasyTotal = fantasyGenre.GetTotalBooks();

               return View();
          }

          private void BuildDisplay(LibraryComponent component, StringBuilder sb, int depth = 0)
          {
               var indent = new string('-', depth * 4);
               if (component is Book book)
               {
                    sb.AppendLine($"{indent}📖 {book.Title} — {book.Pages} pages");
               }
               else if (component is Author author)
               {
                    sb.AppendLine($"{indent}✍ Author: {author.Name}");
                    foreach (var child in author.GetChildren())
                         BuildDisplay(child, sb, depth + 1);
               }
               else if (component is Genre genre)
               {
                    sb.AppendLine($"{indent}📚 Genre: {genre.Name} — Total Books: {genre.GetTotalBooks()}");
                    foreach (var child in genre.GetChildren())
                         BuildDisplay(child, sb, depth + 1);
               }
          }
     }
}