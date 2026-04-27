using System.Collections.Generic;

namespace OnlineLibrary.Flyweight
{
     public class LibraryService
     {
          private readonly BookFormatFactory _factory;

          public LibraryService()
          {
               _factory = new BookFormatFactory();
          }

          public List<LibraryBook> LoadBooks()
          {
               var books = new List<LibraryBook>();

               books.Add(new LibraryBook("1", "Clean Code", _factory.GetOrCreate("PDF", "English", "Penguin")));
               books.Add(new LibraryBook("2", "Design Patterns", _factory.GetOrCreate("PDF", "English", "Penguin")));
               books.Add(new LibraryBook("3", "Refactoring", _factory.GetOrCreate("EPUB", "English", "OReilly")));
               books.Add(new LibraryBook("4", "C# in Depth", _factory.GetOrCreate("PDF", "English", "Penguin")));
               books.Add(new LibraryBook("5", "ASP.NET MVC", _factory.GetOrCreate("EPUB", "Romanian", "Humanitas")));
               books.Add(new LibraryBook("6", "Algorithms", _factory.GetOrCreate("PDF", "English", "Penguin")));

               return books;
          }

          public int GetSharedFormatsCount()
          {
               LoadBooks();
               return _factory.CacheSize();
          }
     }
}