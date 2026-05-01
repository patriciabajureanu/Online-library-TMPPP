namespace OnlineLibrary.Flyweight
{
     public class LibraryBook
     {
          public string Id { get; set; }
          public string Title { get; set; }
          public string Description { get; set; }   // <-- adaugă
          public string ImagePath { get; set; }     // <-- adaugă
          public IBookFormat Format { get; set; }
          public int PublishedYear { get; set; }
          public string CategoryName { get; set; }

          public LibraryBook(string id, string title, string description, string imagePath, IBookFormat format)
          {
               Id = id;
               Title = title;
               Description = description;
               ImagePath = imagePath;
               Format = format;
          }
     }
}