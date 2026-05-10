namespace OnlineLibrary.Visitor
{
     public class Ebook : ILibraryResource
     {
          public string Title { get; set; }
          public int FileSize { get; set; }

          public Ebook(string title, int fileSize)
          {
               Title = title;
               FileSize = fileSize;
          }

          public void Accept(IVisitor visitor)
          {
               visitor.VisitEbook(this);
          }
     }
}