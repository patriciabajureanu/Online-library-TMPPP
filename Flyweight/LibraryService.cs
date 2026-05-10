using System.Collections.Generic;
using System.Linq;
using OnlineLibrary.Data;

namespace OnlineLibrary.Flyweight
{
     public class LibraryService
     {
          public List<LibraryBook> LoadBooks()
          {
               using (var db = new OnlineLibraryDbContext())
               {
                    var booksFromDb = db.Books
                         .Include("Category")
                         .ToList();

                    var books = booksFromDb.Select(b =>
                    {
                         var book = new LibraryBook(
                              b.Id,
                              b.Title,
                              b.Description,
                              b.CoverImageUrl,
                              BookFormatFactory.GetOrCreate(
                                   "PDF",
                                   b.Language ?? "Unknown",
                                   b.PublisherId.HasValue ? b.PublisherId.Value.ToString() : "Unknown"
                              )
                         );

                         book.PublishedYear = b.PublishedYear;
                         book.CategoryName = b.Category != null ? b.Category.Name : "Uncategorized";
                         book.BookType = b.BookType;

                         return book;
                    }).ToList();

                    return books;
               }
          }

          public int GetSharedFormatsCount()
          {
               return BookFormatFactory.CacheSize();
          }
     }
}