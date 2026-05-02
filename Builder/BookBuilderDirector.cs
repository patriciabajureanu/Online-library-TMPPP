using OnlineLibrary.Models;

namespace OnlineLibrary.Builder
{
     public class BookBuildDirector
     {
          private readonly IBookBuilder _builder;

          public BookBuildDirector(IBookBuilder builder)
          {
               _builder = builder;
          }

          public Book BuildBook(string title, string author, string description, string bookType, int pages)
          {
               _builder.Reset();
               _builder.BuildTitle(title);
               _builder.BuildAuthor(author);
               _builder.BuildDescription(description);
               _builder.BuildType(bookType);
               _builder.BuildSpecificDetail(pages);

               return _builder.GetResult();
          }
     }
}