namespace OnlineLibrary.Composite
{
     public class BookComponent : ILibraryComponent
     {
          public string Title { get; set; }
          public int Pages { get; set; }

          public BookComponent(string title, int pages)
          {
               Title = title;
               Pages = pages;
          }

          public string Display(int depth = 0)
          {
               return "<div class='book-node ms-5 mb-2'>📘 " + Title + " — " + Pages + " pages</div>";
          }

          public int GetTotalBooks()
          {
               return 1;
          }
     }
}