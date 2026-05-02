using OnlineLibrary.Models;

namespace OnlineLibrary.Builder
{
     public interface IBookBuilder
     {
          void Reset();
          void BuildTitle(string title);
          void BuildAuthor(string author);
          void BuildType(string bookType);
          void BuildSpecificDetail(int pages);
          void BuildDescription(string description);

          Book GetResult();
     }
}