using OnlineLibrary.Builder.Interfaces;
using OnlineLibrary.Builder.Models;

namespace OnlineLibrary.Builder.Builders
{
     public class MagazineBuilder : ILibraryBuilder
     {
          private Magazine _result = new Magazine();

          public void Reset() => _result = new Magazine();

          public void BuildTitle(string title) => _result.Title = title;

          public void BuildAuthor(string publisher) => _result.Publisher = publisher;

          public void BuildSpecificDetail(int issueNumber) => _result.IssueNumber = issueNumber;

          public Magazine GetResult() => _result;
     }
}