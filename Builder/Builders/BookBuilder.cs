using OnlineLibrary.Builder.Interfaces;
using OnlineLibrary.Builder.Models;

namespace OnlineLibrary.Builder.Builders
{
     public class BookBuilder : ILibraryBuilder
     {
          private Book _result = new Book();

          public void Reset() => _result = new Book();

          public void BuildTitle(string title) => _result.Title = title;

          public void BuildAuthor(string author) => _result.Author = author;

          public void BuildSpecificDetail(int pages) => _result.Pages = pages;

          public Book GetResult() => _result;
     }
}