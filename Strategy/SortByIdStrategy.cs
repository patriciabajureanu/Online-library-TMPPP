using System.Collections.Generic;
using System.Linq;
using OnlineLibrary.Flyweight;

namespace OnlineLibrary.Strategy
{
     public class SortByIdStrategy : IBookSortStrategy
     {
          public List<LibraryBook> Sort(List<LibraryBook> books)
          {
               return books.OrderBy(b => b.Id).ToList();
          }
     }
}