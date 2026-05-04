using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Mediator
{
     public class LibraryMediator : IMediator
     {
          public SearchComponent Search { get; set; }
          public FilterComponent Filter { get; set; }
          public ResultsComponent Results { get; set; }

          private List<Book> _allBooks;

          public LibraryMediator(List<Book> books)
          {
               _allBooks = books;
          }

          public void Notify(object sender, string ev, object data = null)
          {
               if (ev == "search")
               {
                    string text = data as string;

                    var filtered = _allBooks
                        .Where(b => !string.IsNullOrEmpty(b.Title)
                                 && b.Title.ToLower().Contains(text.ToLower()))
                        .ToList();

                    Results.UpdateResults(filtered);
               }

               if (ev == "filter")
               {
                    string filter = data as string;

                    var filtered = _allBooks
                        .Where(b => b.Category != null && b.Category.Name == filter)
                        .ToList();

                    Results.UpdateResults(filtered);
               }
          }
     }
}