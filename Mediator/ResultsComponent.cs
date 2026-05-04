using System.Collections.Generic;
using OnlineLibrary.Models;

namespace OnlineLibrary.Mediator
{
     public class ResultsComponent
     {
          public List<Book> CurrentResults { get; private set; }

          public void UpdateResults(List<Book> books)
          {
               CurrentResults = books;
          }
     }
}