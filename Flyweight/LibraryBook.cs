namespace OnlineLibrary.Flyweight
{
     public class LibraryBook
     {
          public string Id { get; set; }
          public string Title { get; set; }
          public IBookFormat Format { get; set; }

          public LibraryBook(string id, string title, IBookFormat format)
          {
               Id = id;
               Title = title;
               Format = format;
          }
     }
}