namespace OnlineLibrary.Flyweight
{
     public class LibraryBook
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public string Description { get; set; }

          public string ImagePath { get; set; }

          private readonly BookFormatFlyweight _format;

          public LibraryBook(int id, string title, string description, string imagePath, BookFormatFlyweight format)
          {
               Id = id;
               Title = title;
               Description = description;
               ImagePath = imagePath;
               _format = format;
          }

          public string FormatType => _format.FormatType;
          public string Language => _format.Language;
          public string Publisher => _format.Publisher;
          public int PublishedYear { get; set; }
          public string CategoryName { get; set; }
          public int AvailableCopies { get; set; }
          public int TotalCopies { get; set; }
          public int AuthorId { get; set; }
          public string BookType { get; set; }
     }
}