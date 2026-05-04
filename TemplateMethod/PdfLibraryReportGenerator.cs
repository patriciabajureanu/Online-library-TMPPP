using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineLibrary.Data;
using OnlineLibrary.Models;

namespace OnlineLibrary.TemplateMethod
{
     public class PdfLibraryReportGenerator : LibraryReportGenerator
     {
          public PdfLibraryReportGenerator(OnlineLibraryDbContext db) : base(db) { }

          protected override object FetchData()
          {
               return _db.Books
                   .Include("Category")
                   .Include(b => b.Author)
                   .ToList();
          }

          protected override object FormatReport(object data)
          {
               return data as List<Book>;
          }

          protected override string ExportReport(object content, string reportId)
          {
               var books = content as List<Book>;

               var fileName = "LibraryReport_" + reportId + ".pdf";
               var relativePath = "/Content/Reports/" + fileName;
               var fullPath = HttpContext.Current.Server.MapPath(relativePath);

               Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

               using (var stream = new FileStream(fullPath, FileMode.Create))
               {
                    var document = new Document(PageSize.A4, 36, 36, 36, 36);
                    PdfWriter.GetInstance(document, stream);

                    document.Open();

                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
                    var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                    document.Add(new Paragraph("Online Library Books Report", titleFont));
                    document.Add(new Paragraph("Generated from database\n\n", normalFont));

                    var table = new PdfPTable(5);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 3, 2, 2, 1, 1 });

                    AddHeader(table, "Title", headerFont);
                    AddHeader(table, "Author", headerFont);
                    AddHeader(table, "Category", headerFont);
                    AddHeader(table, "Year", headerFont);
                    AddHeader(table, "Copies", headerFont);

                    foreach (var book in books)
                    {
                         table.AddCell(new Phrase(book.Title ?? "-", normalFont));
                         table.AddCell(new Phrase(book.Author != null ? book.Author.FullName : "-", normalFont));
                         table.AddCell(new Phrase(book.Category != null ? book.Category.Name : "-", normalFont));
                         table.AddCell(new Phrase(book.PublishedYear.ToString(), normalFont));
                         table.AddCell(new Phrase(book.AvailableCopies.ToString(), normalFont));
                    }

                    document.Add(table);
                    document.Close();
               }

               return relativePath;
          }

          private void AddHeader(PdfPTable table, string text, Font font)
          {
               var cell = new PdfPCell(new Phrase(text, font));
               cell.Padding = 6;
               table.AddCell(cell);
          }
     }
}