namespace OnlineLibrary.Flyweight
{
     public class BookFormat : IBookFormat
     {
          public string FormatType { get; private set; }
          public string Language { get; private set; }
          public string Publisher { get; private set; }

          public BookFormat(string formatType, string language, string publisher)
          {
               FormatType = formatType;
               Language = language;
               Publisher = publisher;
          }
     }
}