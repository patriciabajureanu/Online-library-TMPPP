using OnlineLibrary.Data;

namespace OnlineLibrary.TemplateMethod
{
     public abstract class LibraryReportGenerator
     {
          protected readonly OnlineLibraryDbContext _db;

          protected LibraryReportGenerator(OnlineLibraryDbContext db)
          {
               _db = db;
          }

          public string GenerateReport(string reportId)
          {
               var data = FetchData();
               var content = FormatReport(data);
               return ExportReport(content, reportId);
          }

          protected abstract object FetchData();
          protected abstract object FormatReport(object data);
          protected abstract string ExportReport(object content, string reportId);
     }
}