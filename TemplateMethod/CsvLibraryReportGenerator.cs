using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.TemplateMethod
{
     public class CsvLibraryReportGenerator : LibraryReportGenerator
     {
          public CsvLibraryReportGenerator(OnlineLibraryDbContext db) : base(db) { }

          protected override object FetchData()
          {
               return _db.Books
                   .Include(b => b.Category)
                   .Include(b => b.Author)
                   .ToList();
          }

          protected override object FormatReport(object data)
          {
               var books = data as List<Book>;
               var sb = new StringBuilder();

               sb.AppendLine("Title,Author,Category,Year,AvailableCopies");

               foreach (var book in books)
               {
                    sb.AppendLine(string.Format(
                        "\"{0}\",\"{1}\",\"{2}\",{3},{4}",
                        book.Title,
                        book.Author != null ? book.Author.FullName : "-",
                        book.Category != null ? book.Category.Name : "-",
                        book.PublishedYear,
                        book.AvailableCopies
                    ));
               }

               return sb.ToString();
          }

          protected override string ExportReport(object content, string reportId)
          {
               var fileName = "LibraryReport_" + reportId + ".csv";
               var relativePath = "/Content/Reports/" + fileName;
               var fullPath = HttpContext.Current.Server.MapPath(relativePath);

               Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
               File.WriteAllText(fullPath, content.ToString());

               return relativePath;
          }
     }
}