namespace OnlineLibrary.Memento
{
     public class ReadingSnapshot
     {
          public int CurrentPage { get; private set; }
          public string Theme { get; private set; }
          public string FontSize { get; private set; }

          public ReadingSnapshot(int currentPage, string theme, string fontSize)
          {
               CurrentPage = currentPage;
               Theme = theme;
               FontSize = fontSize;
          }
     }
}