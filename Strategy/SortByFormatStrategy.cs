using System.Collections.Generic;
using System.Linq;
using OnlineLibrary.Flyweight;

namespace OnlineLibrary.Strategy
{
     public class SortByFormatStrategy : IBookSortStrategy
     {
          public List<LibraryBook> Sort(List<LibraryBook> books)
          {
               return books.OrderBy(b => b.Format.FormatType).ToList();
          }
     }
}