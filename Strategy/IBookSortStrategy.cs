using System.Collections.Generic;
using OnlineLibrary.Flyweight;

namespace OnlineLibrary.Strategy
{
     public interface IBookSortStrategy
     {
          List<LibraryBook> Sort(List<LibraryBook> books);
     }
}