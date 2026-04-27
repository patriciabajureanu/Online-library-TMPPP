using System.Collections.Generic;
using OnlineLibrary.Flyweight;

namespace OnlineLibrary.Strategy
{
     public class BookCatalogContext
     {
          private IBookSortStrategy _strategy;

          public void SetStrategy(IBookSortStrategy strategy)
          {
               _strategy = strategy;
          }

          public List<LibraryBook> SortBooks(List<LibraryBook> books)
          {
               return _strategy.Sort(books);
          }
     }
}