using System.Collections.Generic;
using System.Linq;
using OnlineLibrary.Flyweight;

namespace OnlineLibrary.Strategy
{
     public class SortByTitleStrategy : IBookSortStrategy
     {
          public List<LibraryBook> Sort(List<LibraryBook> books)
          {
               return books.OrderBy(b => b.Title).ToList();
          }
     }
}