namespace OnlineLibrary.Memento
{
     public class ReadingSession
     {
          public int CurrentPage { get; private set; }
          public string Theme { get; private set; }
          public string FontSize { get; private set; }

          public void SetState(int currentPage, string theme, string fontSize)
          {
               CurrentPage = currentPage;
               Theme = theme;
               FontSize = fontSize;
          }

          public ReadingSnapshot CreateSnapshot()
          {
               return new ReadingSnapshot(CurrentPage, Theme, FontSize);
          }

          public void Restore(ReadingSnapshot snapshot)
          {
               CurrentPage = snapshot.CurrentPage;
               Theme = snapshot.Theme;
               FontSize = snapshot.FontSize;
          }
     }
}