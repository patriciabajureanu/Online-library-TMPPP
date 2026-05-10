namespace OnlineLibrary.Visitor
{
     public class Magazine : ILibraryResource
     {
          public string Title { get; set; }
          public int IssueNumber { get; set; }

          public Magazine(string title, int issueNumber)
          {
               Title = title;
               IssueNumber = issueNumber;
          }

          public void Accept(IVisitor visitor)
          {
               visitor.VisitMagazine(this);
          }
     }
}