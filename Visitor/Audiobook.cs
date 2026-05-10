namespace OnlineLibrary.Visitor
{
     public class Audiobook : ILibraryResource
     {
          public string Title { get; set; }
          public int Duration { get; set; }

          public Audiobook(string title, int duration)
          {
               Title = title;
               Duration = duration;
          }

          public void Accept(IVisitor visitor)
          {
               visitor.VisitAudiobook(this);
          }
     }
}